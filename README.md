# ShoppingSpree Application Documentation

#### Powered by:
 1. <a href="https://github.com/wodosharlatan/REST-API-DB"> My REST API </a>
 2. <a href="https://github.com/wodosharlatan/API-Wrapper"> My nugget package </a>

## Home Class

The `Home` class represents the main page of the ShoppingSpree application. It displays a list of entries and allows users to interact with them.

### Constructor

- `Home(string token)`: Creates a new instance of the `Home` class with the specified user token.

### Methods

- `CreateVisualEntry(int listIndex)`: Creates a visual representation of an entry at the specified index in the data lists.
- `UpdateVisualState()`: Updates the visual state of the page by reloading the data from the API and recreating the visual representation of the entries.
- `LoadData(string token)`: Loads the data from the API for the current user.
- `RemoveEntryById(int entryIdToRemove)`: Removes an entry with the specified ID from the API and updates the visual state of the page.

## MainPage Class

The `MainPage` class represents the main page of the ShoppingSpree application. It provides functionality for user sign-up and sign-in.

### Properties

- `api`: An instance of the `API` class used for API communication.
- `SignUp`: A boolean flag indicating whether the user is signing up or signing in.
- `Username`: The username entered by the user.
- `Password`: The password entered by the user.
- `Confirmation`: The password confirmation entered during sign-up.

## Products Class

The `Products` class represents a page in the ShoppingSpree application where users can add new products to their shopping list.

### Properties

- `api`: An instance of the `API` class used for API communication.
- `Count`: The count of the product entered by the user.
- `Unit`: The unit of measurement for the product entered by the user.
- `Product`: The name of the product entered by the user.
- `UserToken`: The user token used for authentication.

### Methods

- `Products(string token)`: Constructor method that initializes the page components and sets the user token.

# Important Notice

In order to use the ShoppingSpree application, you need to create an `APIKey.env` file and place your API key inside it. This file should be located in the same directory as the application files. The API key is required for communication with the API and must be obtained from the API provider. Make sure to secure your API key and never share it publicly.

