
namespace Infrastructure.Repositories
{
    /// <summary>
    /// The Namespace <c>Infrastructure.Repositories</c> is responsible for managing
    /// access to all data for the application through the concrete implementations
    /// of the Repositories defined in the <see cref="Core.Interfaces"/> namespace.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The repositories are kept separate to ensure good separation of concerns, 
    /// and ensure that each repository focuses only on operation for each type
    /// of data, therefore adhearing to the Single Responsibility Principle.
    /// </para>
    /// <para>
    /// The <c>Infrastructure</c> namespace contains all elements of the
    /// Application design model, referred to as the 'Onion Model', considered
    /// to be part of the application dealing with cross cutting concerns.
    /// </para>
    /// <para>
    /// see <a href="http://jeffreypalermo.com/blog/the-onion-architecture-part-1/.">jeffreypalermo.com.</a>
    /// </para>
    /// <para>
    /// This is a separate project within the Application, which therefore holds
    /// all objects in a separate assembly, thus improving the Separation of 
    /// Concerns required for reusable and unit testable code.
    /// </para>
    /// </remarks>
    [System.Runtime.CompilerServices.CompilerGenerated()]
    class NamespaceDoc
    {
    }
}
