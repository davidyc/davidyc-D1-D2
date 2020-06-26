using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace AnalyzerRoslyn
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AnalyzerRoslynAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "AnalyzerRoslyn";

        // You can change these strings in the Resources.resx file. If you do not want your analyzer to be localize-able, you can use regular strings for Title and MessageFormat.
        // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Localizing%20Analyzers.md for more on localization
        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";
        private const string ControllerSuffix = "Controller";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }
        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
        }

        private static void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;
            var mvcController = context.Compilation.GetTypeByMetadataName("System.Web.Mvc.Controller");

            var baseClasses = GetBaseClasses(namedTypeSymbol);
            if (baseClasses.Contains(mvcController) && !namedTypeSymbol.Name.EndsWith(ControllerSuffix))
            {
                var diagnostic = Diagnostic.Create(Rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }

        private static List<INamedTypeSymbol> GetBaseClasses(INamedTypeSymbol namedTypeSymbol)
        {
            var baseClasses = new List<INamedTypeSymbol>();

            var currentTypeSymbol = namedTypeSymbol;
            while (currentTypeSymbol.BaseType != null && currentTypeSymbol.BaseType.TypeKind != TypeKind.Error)
            {
                baseClasses.Add(currentTypeSymbol.BaseType);
                currentTypeSymbol = currentTypeSymbol.BaseType;
            }

            return baseClasses;
        }
    }
}
