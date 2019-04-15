﻿///////////////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2006-2017 Esper Team. All rights reserved.                           /
// http://esper.codehaus.org                                                          /
// ---------------------------------------------------------------------------------- /
// The software in this package is published under the terms of the GPL license       /
// a copy of which has been included with this distribution in the license.txt file.  /
///////////////////////////////////////////////////////////////////////////////////////

namespace com.espertech.esper.epl.script
{
#if false
    public class ScriptCompiler
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Default namespaces
        /// </summary>
        private static readonly string[] DefaultNamespaces =
            {
                "System",
                "System.Collections",
                "System.Collections.Generic",
                "System.IO",
                "System.Text",
                "com.espertech.esper.client",
                "com.espertech.esper.events"
            };

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the references.
        /// </summary>
        /// <value>The references.</value>
        public StringCollection References { get; set; }

        /// <summary>
        /// Gets or sets the imports.
        /// </summary>
        /// <value>The imports.</value>
        public StringCollection Imports { get; set; }

        /// <summary>
        /// Gets or sets the main class containing the static ScriptMain.
        /// </summary>
        /// <value>The main class.</value>
        public string MainClass { get; set; }

        /// <summary>
        /// Gets or sets the name of the root class.
        /// </summary>
        /// <value>The name of the root class.</value>
        public string RootClassName { get; set; }

        /// <summary>
        /// Gets or sets the code to be executed.
        /// </summary>
        /// <value>The code.</value>
        public string Code
        {
            get { return Script.Expression; }
        }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        /// <value>The script.</value>
        public ExpressionScriptProvided Script { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptCompiler"/> class.
        /// </summary>
        public ScriptCompiler()
        {
            RootClassName = "esper" + Guid.NewGuid().ToString("Count",
                CultureInfo.InvariantCulture);
            References = new StringCollection();
            Imports = new StringCollection();
        }

        /// <summary>
        /// Compiles the C# code.
        /// </summary>
        /// <returns></returns>
        public MethodInfo Compile()
        {
            var compilerInfo = CreateCompilerInfo(Language);
            
            var options = new CompilerParameters();
            options.GenerateExecutable = false;
            options.GenerateInMemory = true;
            options.MainClass = MainClass;

            // implicitly reference the NEsper assembly
            options.ReferencedAssemblies.Add(typeof(EPServiceProvider).Assembly.Location);

            // add (and load) assemblies specified by user
            foreach (var assemblyFile in References)
            {
                try
                {
                    // load the assembly into current AppDomain to ensure it is
                    // available when executing the emitted assembly
                    var asm = Assembly.LoadFrom(assemblyFile);

                    // Log the assembly being added to the CompilerParameters
                    Log.Debug("Adding assembly {0}", asm.GetName().Name);

                    // add the location of the loaded assembly
                    if (!string.IsNullOrEmpty(asm.Location))
                    {
                        options.ReferencedAssemblies.Add(asm.Location);
                    }
                }
                catch (Exception ex)
                {
                    throw new ExprValidationException("unable to load assembly", ex);
                }
            }

            var imports = new StringCollection();
            foreach (var import in Imports)
            {
                imports.Add(import);
            }

            // generate the code
            var compileUnit = compilerInfo.GenerateCode(RootClassName, Script, imports);

            var stringWriter = new StringWriter(CultureInfo.InvariantCulture);

            compilerInfo.Provider.GenerateCodeFromCompileUnit(compileUnit, stringWriter, null);
            string code = stringWriter.ToString();

            Log.Debug("Generated script: {0}", code);

            var results = compilerInfo.Provider.CompileAssemblyFromDom(options, compileUnit);

            if (results.Errors.Count > 0)
            {
                throw new ScriptCompilationException("failed to compile script", results.Errors.Cast<CompilerError>().ToList());
            }

            var compiled = results.CompiledAssembly;

            var mainClass = RootClassName;
            if (!string.IsNullOrEmpty(MainClass))
            {
                mainClass += "+" + MainClass;
            }

            var mainType = compiled.GetType(mainClass);
            if (mainType == null)
            {
                throw new ScriptCompilationException(
                    "failed to compile script: unable to find main type");
            }

            var entry = mainType.GetMethod("ScriptMain");
            // check for task or function definitions.
            if (entry == null)
            {
                throw new ScriptCompilationException(
                    "failed to compile script: ScriptMain() not defined");
            }

            if (!entry.IsStatic)
            {
                throw new ScriptCompilationException(
                    "failed to compile script: ScriptMain() not defined as static");
            }

            var entryParams = entry.GetParameters();
            if (entryParams.Length != 1)
            {
                throw new ScriptCompilationException(
                    "failed to compile script: ScriptMain() should have only one parameter");
            }

            if (entryParams[0].ParameterType.FullName != typeof(ScriptArgs).FullName)
            {
                throw new ScriptCompilationException(
                    string.Format("failed to compile script: ScriptMain() takes one member of type {0}, should have only one parameter of type {1}",
                                  entryParams[0].ParameterType.FullName, typeof (ScriptArgs).FullName));
            }

            if (entry.ReturnType.FullName != typeof(Object).FullName)
            {
                throw new ScriptCompilationException(
                    string.Format("failed to compile script: ScriptMain() must return value of type {0}",
                                 typeof(Object).FullName));
            }

            return entry;
        }

        /// <summary>
        /// Creates the compiler INFO.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        private static CompilerInfo CreateCompilerInfo(string language)
        {
            return new CompilerInfo(CreateCodeProvider(language));
        }

        private static CodeDomProvider CreateCodeProvider(string language)
        {
            CodeDomProvider provider;

            switch (language.ToLower())
            {
                case "vb":
                case "visualbasic":
                    provider = CreateCodeDomProvider(
                        "Microsoft.VisualBasic.VBCodeProvider",
                        "System, Culture=neutral");
                    break;
                case "c#":
                case "csharp":
                    provider = CreateCodeDomProvider(
                        "Microsoft.CSharp.CSharpCodeProvider",
                        "System, Culture=neutral");
                    break;
                case "js":
                case "jscript":
                    provider = CreateCodeDomProvider(
                        "Microsoft.JScript.JScriptCodeProvider",
                        "Microsoft.JScript, Culture=neutral");
                    break;
                case "vjs":
                case "jsharp":
                    provider = CreateCodeDomProvider(
                        "Microsoft.VJSharp.VJSharpCodeProvider",
                        "VJSharpCodeProvider, Culture=neutral");
                    break;
                default:
                    // if its not one of the above then it must be a fully 
                    // qualified provider class name
                    provider = CreateCodeDomProvider(language);
                    break;
            }
            return provider;
        }

        private static CodeDomProvider CreateCodeDomProvider(string typeName, string assemblyName)
        {
            Assembly providerAssembly = Assembly.LoadWithPartialName(assemblyName);
            if (providerAssembly == null)
            {
                throw new ArgumentException("unable to find assembly: " + assemblyName);
            }

            Type providerType = providerAssembly.GetType(typeName, true, true);
            return CreateCodeDomProvider(providerType);
        }

        private static CodeDomProvider CreateCodeDomProvider(string assemblyQualifiedTypeName)
        {
            Type providerType = Type.GetType(assemblyQualifiedTypeName, true, true);
            return CreateCodeDomProvider(providerType);
        }

        private static CodeDomProvider CreateCodeDomProvider(Type providerType)
        {
            object provider = Activator.CreateInstance(providerType);
            if (!(provider is CodeDomProvider))
            {
                throw new ArgumentException("invalid provider type");
            }
            return (CodeDomProvider) provider;
        }

        internal class CompilerInfo
        {
            internal readonly CodeDomProvider Provider;

            internal CompilerInfo(CodeDomProvider provider)
            {
                Provider = provider;
            }

            internal CodeCompileUnit GenerateCode(string typeName, ExpressionScriptProvided script, StringCollection imports)
            {
                var compileUnit = new CodeCompileUnit();

                var typeDecl = new CodeTypeDeclaration(typeName);
                typeDecl.IsClass = true;
                typeDecl.TypeAttributes = TypeAttributes.Public;
                typeDecl.BaseTypes.Add(typeof(ScriptBase));

                var scriptMainMember = new CodeMemberMethod();
                scriptMainMember.Attributes = MemberAttributes.Public | MemberAttributes.Static;
                scriptMainMember.Name = "ScriptMain";
                scriptMainMember.Parameters.Add(
                    new CodeParameterDeclarationExpression(typeof (ScriptArgs), "args"));
                scriptMainMember.ReturnType = new CodeTypeReference(typeof (object));

                foreach (var parameter in script.ParameterNames)
                {
                    var variableDeclarationStatement = new CodeVariableDeclarationStatement();
                    variableDeclarationStatement.Type = new CodeTypeReference(typeof (object));
                    variableDeclarationStatement.Name = parameter;
                    variableDeclarationStatement.InitExpression = new CodeMethodInvokeExpression(
                        new CodeVariableReferenceExpression("args"),
                        "GetParameter",
                        new CodePrimitiveExpression(parameter)
                        );

                    scriptMainMember.Statements.Add(variableDeclarationStatement);
                }

                scriptMainMember.Statements.Add(
                    new CodeVariableDeclarationStatement(
                        typeof (object),
                        "__rvalue",
                        new CodeSnippetExpression(script.Expression)
                        )
                    );
                scriptMainMember.Statements.Add(
                    new CodeMethodReturnStatement(
                        new CodeVariableReferenceExpression("__rvalue")
                        )
                    );

                typeDecl.Members.Add(scriptMainMember);

                var nspace = new CodeNamespace();
                // add the default namespaces
                foreach (string import in DefaultNamespaces)
                {
                    nspace.Imports.Add(new CodeNamespaceImport(import));
                }
                // add requested imports
                foreach (string import in imports)
                {
                    nspace.Imports.Add(new CodeNamespaceImport(import));
                }

                compileUnit.Namespaces.Add(nspace);
                nspace.Types.Add(typeDecl);

                return compileUnit;
            }
        }

        public static void VerifyCompilerScript(ExpressionScriptProvided script, string dialect)
        {
            try
            {
                CreateCodeProvider(dialect);
            } 
            catch(TypeLoadException e)
            {
                    throw new ExprValidationException("Failed to obtain script engine for dialect '" + dialect +
                                                      "' for script '" + script.Name + "'");
            }
        }
    }
#endif
}
