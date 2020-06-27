using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace AnalyzerEntities
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AnalyzerEntitiesAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "AnalyzerEntities";

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.AnalyzerTitle), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Resources.AnalyzerMessageFormat), Resources.ResourceManager, typeof(Resources));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Resources.AnalyzerDescription), Resources.ResourceManager, typeof(Resources));
        private const string Category = "Naming";
        private const string IDPropertyName = "ID";
        private const string NamePropertyName = "Name";
        private const string AttributeName = "DataContractAttribute";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
        }

        private static void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            var namedTypeSymbol = (INamedTypeSymbol)context.Symbol;
            var name = context.Symbol.ContainingNamespace.Name;
            var x = namedTypeSymbol.Name;
            if (name == "Entities" && (!CheckPropertyName(namedTypeSymbol, IDPropertyName) 
                || !CheckPropertyName(namedTypeSymbol, NamePropertyName) || !CheckAttribute(namedTypeSymbol, AttributeName)))         
            {
                var diagnostic = Diagnostic.Create(Rule, namedTypeSymbol.Locations[0], namedTypeSymbol.Name);
                context.ReportDiagnostic(diagnostic);
            }
        }

        private static  bool CheckPropertyName(INamedTypeSymbol namedTypeSymbol, string proppertyName)
        {
            var members = namedTypeSymbol.GetMembers();
            foreach (var member in members)
            {
                if (proppertyName == member.Name)
                    return true;
            }
            return false;
        }

        private static bool CheckAttribute(INamedTypeSymbol namedTypeSymbol, string attributteName)
        {
            var attributes = namedTypeSymbol.GetAttributes();
            foreach (var attribute in attributes)
            {
                if (attribute.AttributeClass.Name == AttributeName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
