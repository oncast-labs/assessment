General information:

  Solution create using: Microsoft Visual Studio Ultimate 2013 - Version 12.0.31101.00 Update 4
  Installed Framework: Microsoft .NET Framework Version 4.5.51209
  Solution projects target Framework Version: 4.5

Dependencies:

  Json.NET: installed via NuGet (also available @ https://www.nuget.org/packages/Newtonsoft.Json/)

Architecture design choices:

  The solution is divided in two projects, one being the assessment itself and the other a simple test project.

  Test project: in this project, the required tests have been implemented and all pass successfully. This can be
                verified by running said tests, via VS2013's 'TEST' menu, 'Test Explorer', etc.

  Assessment project: broken down into various modules, each in one directory. These are:

    Commands: helper classes for the ViewModels; add support for easy mapping of commands.

	Configuration: classes that deal with the service configuration. The FileConfigurationLoader, as per its name,
                   implements the functionality of opening, reading and parsing a file in order to load its entries.
				   The OrderingConfiguration class is responsible for converting string-resources into the actual
				   object used in the ordering.

    Models: contains both the Book and Shelf classes - simple classes that represent the problem's model. The Shelf
	        class comes with a helper method to add the default books, those that were parameterized in the assessment.

    Resources: contain string-resources, stored in the json format, in order to facilitate the configuration process.
	           There are also pre-made configuration files, those correspond to the configurations set - and used - in
			   the tests cases.

    Services: various classes that support or implement the ordering service. The IIOServices and IOServices are the
	          exception; they implement the functionality needed for dependency injection into the main window view
			  model - since the view model shouldn't concern itself with the view, and the dialog to select a file
			  falls into the view category, those are separated. Ideally, in a 'real' project, the interface would
			  be declared in the view model project (or in a project that is a dependency of the view model),
			  its implementation would be implemented by the view project (or any project that adds functionality 
			  to the view). In this case, this separation would be more evident, albeit with similar fuction.
			  The ordering mechanism is implemented using c#'s feature of providing an implementation of IComparer,
			  in this case, IComparer<Book>. Sorting lists then becomes simply the task of setting up an instance
			  of this class and using it in the appropiate List.Sort() overload.

    ViewModels: the various classes representing the models, responsible for presenting data to the view, binding the
                model and implementing the commands bound to the view. One of those classes, the BaseViewModel, is an
				abstract class that implements INotifyPropertyChanged. By inheriting from the base class, the rest of
				the view models can call upon RaisePropertyChanged where necessary (usually on properties' set accessors).

    View: XAML code for the sole window in the project. Simply defines various UI elements and their bindings to the
	      main window view model. Note that the main window view model is created via XAML as well, and thus the IOServices
		  is as well - this is the instance that is read and used as IIOServices in the view model.

It is worth mentioning that the MVVM design adopted allows a complete Model - View separation. In the test projects, for
example, one could expand the testing coverage by including instances of the view models themselves. The correctness of
the tests are guaranteed, in part, because the view doesn't manipulate the data in any way that isn't viable to do via
unit testing - meaning one does not need to programatically click buttons or drag selections accross the interface. The
logic is contained in the model + view model, to the extent that multiple views can be bound into the system and their
behaviour would be guaranteed to be the same (save for UI specific code, of course).

In the assessment presented, there is no code behind on any UI windows / files. This is also part of the design, by
minimizing the code behind and allocating this logic to the view model, a greater decoupling scenario is achieved.