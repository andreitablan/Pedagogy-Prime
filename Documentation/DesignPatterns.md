# Design patterns

## 1) Mediator

The Mediator design pattern is a behavioral design pattern that promotes loose 
coupling among a group of objects by centralizing communication between them. It acts as an intermediary that coordinates interactions between objects, helping to reduce the direct dependencies and connections between individual components in a system.
		
There are some core benefits of using the Mediator design pattern:
1. Decouples Components: The Mediator pattern helps reduce the direct dependencies between objects. Instead of objects having to be aware of and interact with multiple other objects, they only need to interact with the Mediator. This promotes a more modular and maintainable design.
2. Simplifies Communication: By centralizing communication, the Mediator simplifies how objects interact. It eliminates the need for complex and potentially error-prone inter-object communication code, making the system easier to understand and modify.
3. Promotes Reusability: Since the communication logic is encapsulated in the Mediator, it can be reused across different parts of the system, making it more efficient to add new components or change existing ones.
		
In our application, we will use the Mediator design pattern on the backend side for communication between the presentation layer (API) and business layer (Infrastructure). 

This pattern works extremely well alongside CQRS (Command Query Responsibility Segregation), an architectural pattern used for decoupling the business logic in Commands (logic for altering the exposed data) and Queries (logic for retrieving data).
By using this to patterns together our application will have a boost of performance because we will have low coupling and high cohesion.

## 2) Repository

The Repository design pattern is a structural design pattern used in software development to separate the logic that retrieves data from a data store (such as a database) from the rest of the application. 

It provides a higher-level interface for accessing data and abstracts away the details of how data is retrieved, stored, and managed. The primary goal of the Repository pattern is to create a clear separation of concerns and promote a more maintainable and testable codebase.

The core benefits of the Repository design pattern:
1. Separation of Concerns: The pattern promotes a clear separation between the data access code and the rest of the application's business logic, making the codebase more maintainable and easier to understand.
2. Abstraction: Clients interact with the repository interface, which provides an abstract layer for data access. This abstraction allows you to change the data storage mechanism or implementation details without affecting the client code.

4. Reusability: Repositories can be reused across different parts of the application, ensuring consistent data access patterns and reducing code duplication.
5. Centralized Data Access Logic: Data access logic, including error handling and query construction, is centralized within the repository implementation, making it easier to manage and maintain.
6. Scalability: In larger applications, the Repository pattern helps manage data access efficiently and can be extended to handle caching, data synchronization, or other advanced data access requirements.

In our application we will use Repository design pattern for data access code. We will implement a generic Repository design pattern for better legibility for reusability for lower coupling and more other advantages which comes with using this design pattern.

## 3) Proxy
The Proxy design pattern is a structural pattern that provides a placeholder for another object to control access to it. This acts as a middleman between a client and a real object, allowing the proxy to intercept requests, perform necessary actions, and then delegate the request to the real object.

**Page Access/Resource Access for Non-Logged-In Users**: When a non-logged-in user tries to access the page, the proxy recognizes the user's unauthenticated status. The proxy can then display a login form or a message asking the user to log in Once the user logs in, the proxy grants access to the real special page content. Also, this can be used to check the user's permissions and roles to determine if they are authorized to access the resource. For example, a student can access the materials only from the subjects from his year of study. 

**Lazy load components**: We are working with files which can be a heavy resource. The proxy loads heavy resources only when they are needed, improving application performance (for loading materials and images with the exercises from the laboratory).

## 4) Template 
The Template Method is a behavioral design pattern that defines the structure of an algorithm or a process while allowing specific steps to be implemented by subclasses.

**Quizzes component**: This template establishes a framework for creating quizzes and surveys. It covers steps for adding questions, setting scoring rules, and generating results. Subclasses can adapt the template to handle various question types and scoring mechanisms, such as multiple-choice quizzes or free-text surveys.

**Repository level**: We can use this design pattern at the repository level to standardize the workflow for data access and database operations. By doing so, the common database operations follow a consistent structure, while allowing specific queries or interactions to be customized by individual repository subclasses.

**Content Publishing Workflow**: the courses for the subjects can have multiple formats. Using this design pattern, we can parse this information generate the necessary data for our analyzer, and create the subject coverage. 

## 5) Factory
The Factory pattern is a creational pattern used to create objects without specifying the exact class of object that will be created. This pattern can be used in various situations within the app.

An example would be the User creation, where we would create a user class depending on the provided type: admin, teacher, student, thus encapsulating the creation logic. Another example would be for the forums where we have 2 different types of classes: SubjectForum and CourseForum, the difference being that one is for the whole Subject and one for a specific lecture within the selected subject. With Factory we could handle this creation altogether encapsulating the creation for those two as well. 
## 6) Decorator 
Decorator is a structural design pattern that allows you to attach new behavior to already existing objects, without affecting the other objects from the same class. This change can either be done statically or dynamically. 

In our application, decorator could be used for adding more functionality to our forums and messages that are found in them. For example, we could use it to improve forum caching helping with performance in case of a heavy load of messages that can be found there. 

Another example would be adding additional info to some of our messages in the forums. That can be either some tags like importance, some additional notes, an attachment for the message or just some reply to it.

Decorator is also a great design pattern because of its similarity with proxy and usability with it and also chain of responsibility. 

## 7) Visitor
Visitor is a behavioral design pattern that is used to separate an algorithm from the object structure. This way we can use advanced operations on already created objects without modifying them. This is usually done via traversing the object structure to perform these operations. 

Examples of the usage of this pattern in our application would be for the calculations of the statistics for the students depending on the grades for their homeworks. 

Another example is validating that all the homeworks have a correct grade assigned to them. Lastly, we can use Visitor to export these statistics in some formats: JSON, XML, etc.

## 8) Observer
The Observer pattern is useful to establish a one-to-many dependency between objects. In the application, we can use the Observer pattern to notify various components or users about updates, such as when new course materials are uploaded, or when a course description is modified.
An example could be if we have a notification system to inform users (professors and students) about changes in course materials. We can implement this using the Observer pattern. 

The Course class can act as a subject, and users can subscribe as observers to receive notifications. When a new course material is uploaded or the course description is edited, the Course class notifies all registered observers. Another example could be at the scheduling of a class, we also inform students about this event.

It can interact with other design patterns such as the Mediator pattern which could also be used to centralize communication between different components, including notifying observers. The Mediator can help separate the subject (e.g. Course) and observers, simplifying the interactions.
## 9) Chain of Responsability 

The Chain of Responsibility pattern is useful when we want to pass a request through a chain of handlers. In the application, we can use this pattern to process and validate uploaded files, ensuring they attach to the required format.

We can create a chain of responsibility for file upload processing. Each handler in the chain checks a specific aspect of the uploaded file, such as format, content, and structure. If a handler cannot process the file, it passes the request to the next handler in the chain until a suitable handler is found. For instance, the first handler checks if the uploaded file is a valid format (PDF or Word), the second handler parses the file structure, and the third handler validates the content against the course description.

It can interact with other design patterns such as: 
- **Repository**: The Repository pattern can be used to interact with the database for file storage and retrieval, ensuring a separation of concerns between data access and file processing.

- **Observer**:  When a file is processed by the chain of responsibility, the result can be observed by the registered users, providing transparency in the file processing workflow and notifying the user about the success or failure of the upload. 