# Team7-Project

Study Timer is a motivational tool for studying that goes beyond a simple countdown application. It allows you to assign and categorize tasks, while providing a visually stimulating image to keep you motivated. Say goodbye to distractions and hello to productivity with Study Timer!


## Features

- ### The Repository Pattern
    The Repository Pattern restricts access to the database and ensures that database operations are performed only within the framework of specified methods, thus enhancing database security.
- ### Identity Mechanism
    The Identity Mechanism is a user management system. The Identity Mechanism provides common fields, such as email and username, as well as methods for registering and logging in that are applicable to all users.
- ### Fluent Validation
    Fluent Validation is used to ensure that user input values are correct and valid. If the values are incorrect, a warning message is displayed to the user. Fluent Validation ensures that only correct and valid data is stored in the database.
- ### Toaster Notifications
    Toaster Notifications is a service that provides users with toast messages, which appear as bubbles in the upper right corner. This service informs the user about system situations.
- ### Statistics
    Users can track their progress on the Statistics page. On this page, users can view their total number of sessions, total work time, most frequently worked topic, total number of completed and incomplete tasks, and last work time. This information will motivate users to continue working.

## Features Details

- ### The Repository Pattern
    We implemented the Repository Pattern by creating the IRepository class under the Application project in the Core layer.The IReadRepository and IWriteRepository interfaces inherited from the IRepository. We then developed the read and write interfaces of the entities. Then we wrote the classes that implement these interfaces under the Persistence project in the Infrastructure layer of the project. We collected the read and write classes of each entity in ReadRepository and WriteRepository to avoid code repetition. Finally, we used them in the appropriate places in the MVC project in the Presentation layer of the project.
- ### Identity Mechanism
    First, we added the Microsoft.Extensions.Identity.Stores package under the Domain project in the Core layer of the project. Then we defined the Role and User classes for Identity. Next, we added the Microsoft.AspNetCore.Identity.EntityFrameworkCore package under the Persistence project in the Infrastructure layer of the project. We wrote the field constraints of the User class. Then, in the Presentation layer of the project, in the MVC project, we performed the registration, mail confirmation, login and logout operations in AuthManager using UserManager and SignInManager. In the StudySessionManager, we utilized the UserManager to retrieve the ID of the logged-in user. Additionally, we verified that the user is logged in and that the account is valid for the required actions.
- ### Fluent Validation
    To add the Fluent Validation library to the project, we installed the required package for ASP.NET Core. To do this, we installed the required package for ASP.NET Core and specified the classes where Fluent Validation will be used. In Program.cs, we added the Identity Service and implemented Fluent Validation for Register, Login, and AddDuty operations.   We created a separate class for each validation under the Validators folder and implemented it from the AbstractValidation class. In the AbstractValidation class, we specified the class on which we want to apply validation. We added the necessary validation requirements for each property using the RuleFor method in the constructor and the appropriate validation methods. Toaster notifications are also included.  In each class, we identified the properties that require validation for a specific operation (Register, Login, or addDuty).
- ### Toaster Notifications
    Initially, the NToastNotify package was installed in the MVC project's Presentation layer. Next, an interface was developed to specify the required functions, and a class was implemented to avoid dependency on the package. Subsequently, IToastService was utilized in the relevant Controllers. The Toast Notify component was then added to _Layout.cshtml. Finally, the necessary services were added to Program.cs to install the NToastNotify package.
- ### Statistics
    First, we defined the fields that will be displayed to users in HomeGetStatisticsViewModel. We used Repositories in StudySessionManager to fill in these fields. Afterwards, we displayed the fields in the Statistics view for the users.

## Task Breakdown
- ### [Project To Do Kanban](https://github.com/users/ozgedincer/projects/2)
  
- ### [Hakkıcan Bülüç](https://github.com/MrBuluc)
    RepositoryPattern, Entities, Controllers, Request and Response models, AuthManager, EmailService, ToastService, StudySessionManager
- ### [Zehra Bengisu Doğan](https://github.com/Bengisoo)
    Fluent Validation, Controllers, Category Selection Mechanism, Configurations, Identity Mechanism, Database Design, Migrations, Navigation Property
- ### [Özge Dinçer](https://github.com/ozgedincer)
    Database Design, Entities, Controllers, Identity Mechanism, Statistics, User Study Session, Fluent Validation, Toaster Notifications
  - ### [Seyyit Ahmet Kılıç](https://github.com/sahmett)
    Configurations, Database Design, Entities, Controllers, Identity Mechanism, Statistics, User Study Session, Toaster Notifications, StudySessionManager




