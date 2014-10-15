#MvvmCrudGv

(Yet another .Net Wpf Mvvm CRUD application – hands on in 14 easy steps for WPF/MVVM beginner.)

MvvmCrudGv is a basic CRUD app (todo lis application) written in .Net wpf MVVM pattern. The Framework (or we would rather call it implementation) created here is absolutely minimalistic with only basic features needed for our MVVM CRUD application to work.

Below are the steps we performed to create this project:-

 1. Create a wpf project in Visual Studio we will call it MvvmCrudGv. (lots of quickstarts available online). 
 Add another project type ClassLibrary to this solution, call it MvvmCrudGv.Service. Add reference System.ServiceModel, System.Runtime.Serialization to service.
	-(We will add our Persistence service here. We can write our own Data layer from here onwards to persist the service objects.)
 2. Create these folders - Views, ViewModels, Common 
	- ViewModels, Views,Common, Common>Behaviors, Common>Messaging (Viewmodels store our viewmodels, common stores common files, View stores views)
	- Entity, Persistence in MvvmCrudGv.Service
	- Add a our simple Todo (our basic DataContract which we want to persist) to MvvmCrudGv.Service.Entity. In MainWindow.xaml.cs we create a dummy list of these Todo's and bind to this dummy list in MainWindow.xaml to test it.
	- Test the MainWindow's is binding to DataContext (TodoList).

 3. Move MainWindow to Views folder. In App.xaml point the startup uri to correct place  (StartupUri=Views/MainWindow.xaml). Test it is working.
 4. Add a class ViewModels\MainWindowViewmodel. We want our MainWindow.xaml to use this class as its ViewModel/DataContext.
 5. Add Common\ViewModelLocator.  This will be the common class which will define our View=>ViewModel routings.
	 - This is our injector/mapper of Views=>ViewModels. 
	 - Expose public property type MainWindowViewModel.
	 - We will put it in a common place in "App.xaml" resource dictionary so that anybody can make use of this class anytime.
	 -	 In App.xaml add the ResourceDictionary  
        <ResourceDictionary>
            <vm:ViewModelLocator x:Shared="False"  x:Key="Locator" xmlns:vm="clr-namespace:MvvmCrudGv.Common" />
        </ResourceDictionary>
     - Bind the Views/MainWindow.xaml to ViewModels\MainWindowViewModel
		- In Views/MainWindow.xaml  bind the property DataContext="{Binding MainWindowViewModel, Source={StaticResource Locator}}"
		- We will see the display of list will disappear because the DataContext has changed and we should move our bindings to its ViewModel now.
			- Remove the TodoList logic from MainWindow.xaml.cs and move it to MainWindowViewModel.cs. Set the ListView ItemsSource="{Binding TodoList}". Test that view is displaying the list correctly now.
 Milestone 1: Basic Setup ready.
 
 6. Refractor to include navigation between pages. This probably is the most important function of our App.
	- Add IEventAggregator and EventAggregator Classes.
	- Add the NavMessage class. This is a class which can be used to publish/subscribe a Navigation event.
	
 7. Our EventAggregator or messaging system is ready. Now there is time to use it.
	- Create a new Views Home.xaml, TodoDetails.xaml and HomeViewModel, TodoViewModel.  The TodoDetails view we will use later.
	First of all let us use Home view. Our MainWindow will straight away navigate to Home view once loaded. Let us create the frame and hook the navigation code to our MainWindow.
	- Move the view code from MainWindow.xaml to Home.xaml. Move the code from MainWindowViewModel to HomeViewModel.
	- Our MainWindow.xaml will have a menu strip at top and a Frame Name=MainFrame below it. Let us add the code.
	- Create the properties for HomeViewModel and TodoViewModel in ViewModelLocator. Bind Home.xaml and TodoDetails.xaml to their ViewModels.
	- In App.xaml.cs add a static IEventAggregator. On startup of application instantiate this eventAggregator.
	- In the MainWindow.xaml.cs use this App.eventAggregator to subscribe to events of type NavMessage, and perform navigation on the MainFrame according to NavMessage.
 
 Milestone 2: Mvvm Navigation ready.
 
 8. Our framework is now actually ready for our CRUD operations. Let us write the actual Service and Persistence logic now. To Project MvvmCrudGv.Service add the ServiceContract and its implementation. (ITodoService and TodoService resp)	 
 9. Add a singleton Common\BootStrapper. Add start and exit bootstrap routines to this. Enable BootStrapper in App.xaml.cs (OnStartup and OnExit)
	- Bootstrapper holds a static instance to TodoService .In real world we would want to use proper dependency injection and use it to instantiate ITodoService whenever we require. Here we will use BootStrapper's static TodoService instance whenever we want.
	- Let us test the code if it is working. Move the dummy list filler to TodoService. Use TodoService to fill the list in HomeViewModel.
	- Test the TodoService is now working. Lets write our CRUD display and functionality now.
 
 Milestone 3: Added Service.
 10. Now we need some helper classes to create our CRUD operations. What we will be doing now is pretty much standard MVVM CRUD operations.
	-Let us first of all add our Common\BaseViewModel abstract class. This class implements INotifyPropertyChanged which is used by WPF/Xaml to dynamically bind property changes with view. This class also has a IsDirty flag which is marked true as soon as something is modified.
	-Add \Common\RelayCommand. The purpose of this class is as its name suggests - it will relay the command to appropriate Delegate defined in instance. There is a lot of information available online about this class.
	-One thing handy to WPF/Xaml are converters. Sometimes what Xaml wants are specific types (like Visibility, Color ) and we want to bind these to our properties of different kind (like a boolean flag IsVisible).
	Now to convert from one Type to other (the one required by Xaml) we implement Converters to convert back and provide Xaml with what it really wants. We will add some Converters here. We can notice tese Converters are converting from one type to other.
	-Add Common Styles in \Styles.xaml and add its reference to App.xaml (<ResourceDictionary Source="Styles.xaml" />). Add commonly used converters & styles here in ResourceDictionary.
 
 11. The Next thing we want to do now is to update our TodoViewModel. This is the basic ViewModel class with which our MVVM application interacts.
	- As MVVM will require some presentation related properties and commands, and we don't want to add these presentation properties to our Service Entities, 
	we will wrap our Service/Domain entities with ViewModel wrapper. Our TodoViewModel is basically a wrapper around Service.Entity.Todo. Our MVVM will interact with TodoViewModel
	which in turn will modify some of the properties of Enclosed Service.Entity.Todo. When we want to talk to our Service, we will simply remove the wrapper (TodoViewModel properties) and
	send over the enclosed Service.Entity.Todo to the service.
	-Let us add our TodoViewModel wrapper now. This Class inherits from BaseViewModel. TodoViewModel contains some MVVM properties as well.

 12. We add the CRUD operations now:
	- We will create our Home.xaml now with basic CRUD operations display. Notice that the buttons are not working as commands are not bound to real delegates yet.
	- Add the CRUD operations in HomeViewModel.
		- Add Default and custom ErrorTemplate(textBoxErrorTemplate) to Styles.xaml and use this template for validation of Textboxes. 
		- Add a simple NumericValidationRule and update the Binding of target TextBoxes.
		- Add simple Event To Command (Behavior) DoubleClick and hook it to ListView's items DoubleClick takes us to details page.
 13.  Next up we will create the TodoDetails.xaml and Details page should take its DataContext from supplied TodoViewModel. 
	- There is some basic catch we need to notice here regarding Navigation. On load of the view we are overriding the DataContext with the supplied ViewModel instead of default one. Notice the changes in TodoDetails parametrized constructor TodoDetails.xaml.cs.
	- Notice the Back and Save buttons functionality in TodoViewModel bound to TodoDetails view.
 
 Milestone 4: Added MVVM CRUD Operations.
 
 14. Add the Persistence layer.
	- We right click the MvvmCrudGv.Service project, "Manage Nuget Packages...." and installed "protobuf-net" package here.
	- We added the TodoPersistence class and associated ProtobufDB and helper classes. Also update our DataContract "Todo" with [ProtoContract],[Serializable] and its properties with [ProtoMember(<int>)] attributes.
	- Update the ITodoService to use TodoPersistence instead of simple in-memory list.