
namespace IoC
{
    /// <summary>
    /// Namespace <c>IoC</c> is responsible for the configuration of the 
    /// Dependency Injection (DI) configuration.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It is kept as a separate project so that all layers in the application
    /// can adhere to the architectural pattern known as the 'Onion Model'.
    /// A layer can only depend on componenetsthat are closer the core
    /// However, the UI layer depends on the Core layer for the infrastructure
    /// componenets like repositories, and needs access to the concrete implementation 
    /// of those repositories.
    /// </para>
    /// <para>
    /// see <a href="http://jeffreypalermo.com/blog/the-onion-architecture-part-1/.">jeffreypalermo.com.</a>
    /// </para>
    /// <para>
    /// The entire model is surrounded by a "Dependency Resolution" layer, which
    /// is implemented here with this Namespace, where such dependencies on the 
    /// infrastructure are injected at runtime.
    /// </para>
    /// <para>
    /// This application makes use of Microsoft Unity to provide the DI.  The specific
    /// version is 3.0.1208-Preview, which is a pre-release version and the only 
    /// version that works with Windows Store App (targetFramework is "winrt45").
    /// </para>
    /// </remarks>
    [System.Runtime.CompilerServices.CompilerGenerated()]
    class NamespaceDoc
    {
    }
}
