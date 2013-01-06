//***************************************************************************************************
//Name of File:     App.xaml..cs
//Description:      Code behind the Application start module.
//Author:           Tim Harrison
//Date of Creation: Dec 2012.
//
//I confirm that the code contained in this file (other than that provided or authorised) is all 
//my own work and has not been submitted elsewhere in fulfilment of this or any other award.
//***************************************************************************************************

using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

using Microsoft.Practices.Unity;            //  DI container.
using THLibrary.Infrastructure.Unity;       //  IoC configuration UI project.
using Core.Interfaces;                      //  Interfaces for Infrastructure elements of the Infrastructure project.

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace THLibrary
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    /// <remarks>
    /// MS Unity is used to provide Dependency Injection into the various classes where
    /// required.  The container has to be exposed through a property the xaml based UI
    /// and code behind does not support parameterless constructors and we can therefore
    /// not inject the dependencies directly.
    /// This workaround applies to the Repository instances also.  
    /// Further investigation must be done to see how to implement Di through properties
    /// to see if that is a better option and could remove the need to expose the instance
    /// through Public properties of the App class.
    /// <para>
    /// Since this is a single page application there is no neded to saving any state 
    /// information as it is not possible to navigate away from the page.  Data is loaded
    /// at application start and the Searches, if there are any, are saved at application
    /// suspend.
    /// </para>
    /// </remarks>
    sealed partial class App : Application
    {
        //  Holds the instance of the IoC container.
        private readonly IUnityContainer _ioCcontainer;
        //  Holds the instances of the Library and Search Repositories.
        private readonly ILibraryRepository _libraryRepository;
        private readonly ISearchRepository _searchRepository;


        /// <summary>
        /// Gets the instance of the IoC Container for resolbing the dependencies within
        /// each module.  It uses MS Unity as the Dependency Resolbr.
        /// </summary>
        public IUnityContainer IoCContainer
        {
            get
            {
                return _ioCcontainer;
            }
        }
        /// <summary>
        /// Gets the instance of the LibraryRepository for access to the underlying 
        /// data model: specifically the Library Book data.
        /// </summary>
        public ILibraryRepository LibraryRepository
        {
            get { return _libraryRepository; }
        }
        /// <summary>
        /// Gets the instance of the SearchRepository for access to the underlying
        /// data model: specifically the Search Data.
        /// </summary>
        public ISearchRepository SearchRepository
        {
            get { return _searchRepository; }
        }


        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            //  TODO: Add handler for OnSuspending to save the search stuff to localfolder.  IF WE CAN????????

            //  Instantiate the Unity IoC container 
            _ioCcontainer = new UnityContainer();
            //  Now configure it,set up the mappings
            UnityConfiguration.RegisterTypes(_ioCcontainer);

            //  Instantiate the Library Repository
            _libraryRepository = _ioCcontainer.Resolve<ILibraryRepository>();

            //  INstantiage the Search Repository.
            _searchRepository = _ioCcontainer.Resolve<ISearchRepository>();
            //  Instantiate another repository, to check that the lifetime is OK, 
            //  it shouldn't run the contructor of the Library module.  IT DOESN'T YEH!!!!!!!!!!
            //var _testRepository = _ioCcontainer.Resolve<ILibraryRepository>();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity

            //  Close the Searches Database.
            _searchRepository.CloseData();

            //  This is the code from the SearchData Close method
            //SaveSearches("SearchesData.xml", _searchRepository.GetSearches());

            deferral.Complete();
        }

    }
}
