# Technologies

- ASP.NET Core 7.0
- Angular 14
- Mock database operations using `BlogData.json`
- LINQ

# Setup Instructions

1. Install the necessary packages for Angular, including Node and npm.
2. Run the command `npm install` to install the required packages for the UI project.
3. Set the appropriate `baseUrl` in the `blog-data.service.ts` file to link the API to the UI.
4. Start both the UI and Web API applications.

# Application Features

- CRUD operations for blogs with UI routing and component communication using `@Input` and `@Output`.
- Additional features implemented: searching (user can search the blogText/username) and pagination(user has control to change the page.As per current implementation per page 3 blogs are visible).
- The `BlogService.cs` in the DataService folder mocks the database operations, with CRUD operations reflected in the `BlogData.json` file.
- Utilizes LINQ for data querying.

# Design Decisions and Application Structure

### Application Structure

1. The UI is organized into higher-level folders such as `components`, `interfaces`, `custom pipes`, and `services`. The API is structured into folders like `controllers`, `managers`, `models`, and `data service`.
   
2. User feedback messages are provided using simple `window` functions in the UI to meet current objectives.

3. All required validations are implemented using reactive forms at the UI level to minimize unnecessary HTTP calls to the API for simple validations.

# Performance Considerations & Future Scope

1. **Pagination:** To handle large numbers of blogs efficiently, we plan to implement server-side pagination. This will reduce the amount of data transferred to the UI and improve loading times for users.We can modify the `GetBlogs()` method to accept `blogsPerPage` and `pageNumber` as parameters to reduce the load on the browser.

2. **Lazy Loading:** Future plan for lazy loading of modules in future multi-module Angular applications. Implement lazy loading in the API when dealing with relational tables. This can be achieved using `loadChildren()` in the UI and virtual properties at the API level.

3. **Efficient Queries:** Use efficient LINQ/SQL queries and proper indexing to enhance performance. Consider creating Data Access Objects (DAOs) instead of using a single DataService to manage database operations.

4. **Data Compression:** Since blog details may contain large amounts of data, implement compression to reduce payload size.We can do the compression using `ResponseCompression` nuget package. 

5. **Logging & Exception Handling:** Logging and exception handling are crucial for large-scale applications. Implement logging to files, databases, or cloud services based on requirements. In the future, consider creating a custom exception handler that generates user-friendly messages instead of technical error details. Also in future instead of simple `window` functions we can incorporate toaster to show messages more UI friendly.

# Testing (Future Scope)
Unit and integration tests will be implemented using XUnit/NUnit for API.

