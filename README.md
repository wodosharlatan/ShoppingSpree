# ShoppingSpree Application Documentation

## Powered by:
 1. <a href="https://github.com/wodosharlatan/REST-API-DB"> My REST API </a>
 2. <a href="https://github.com/wodosharlatan/API-Wrapper"> My nugget package </a>

## Home Class

The `Home` class represents the main page of the ShoppingSpree application in .NET MAUI. It displays a list of entries and allows users to interact with them.

### Constructor

#### `Home(string token)`

Creates a new instance of the `Home` class with the specified user token.

**Parameters**
- `token` (string): The user token to authenticate API requests.

### Methods

#### `CreateVisualEntry(int listIndex)`

Creates a visual representation of an entry at the specified index in the lists of data. This method creates labels, checkboxes, and layout containers to display the entry information.

**Parameters**
- `listIndex` (int): The index of the entry in the data lists.

#### `UpdateVisualState()`

Updates the visual state of the page by reloading the data from the API and recreating the visual representation of the entries.

#### `LoadData(string token)`

Loads the data from the API for the current user.

**Parameters**
- `token` (string): The user token to authenticate API requests.

**Returns**
- `Task<int>`: A task that represents the asynchronous operation. The task result is the number of entries loaded.

#### `RemoveEntryById(int entryIdToRemove)`

Removes an entry with the specified ID from the API and updates the visual state of the page.

**Parameters**
- `entryIdToRemove` (int): The ID of the entry to remove.

## MainPage Class

The `MainPage` class represents the main page of the ShoppingSpree application. It provides functionality for user sign-up and sign-in.

### Properties

- `api`: An instance of the `API` class used for API communication.
- `SignUp`: A boolean flag indicating whether the user is signing up or signing in.
- `Username`: The username entered by the user.
- `Password`: The password entered by the user.
- `Confirmation`: The password confirmation entered during sign-up.

### Methods

#### `MainPage()`

Constructor method that creates an instance of the `API` class and initializes the page components.

#### `CreateAPIinstance()`

Asynchronously creates an instance of the `API` class by loading the API key from a file.

#### `LoadMauiAsset()`

Asynchronously loads the API key from the "APIKEY.env" file.

#### `SignUpButton_Clicked()`

Event handler for the sign-up button click event.

#### `SignInButton_Clicked()`

Event handler for the sign-in button click event.

#### `SubmitButton_Clicked()`

Event handler for the submit button click event. Performs sign-up or sign-in based on the user's choice.

#### `ConfirmEntry_TextChanged()`

Event handler for the password confirmation entry text changed event.

#### `PasswordEntry_TextChanged()`

Event handler for the password entry text changed event.

#### `NameEntry_TextChanged()`

Event handler for the username entry text changed event.

## Products Class

The `Products` class represents a page in the ShoppingSpree application where users can add new products to their shopping list.

### Properties

- `api`: An instance of the `API` class used for API communication.
- `Count`: The count of the product entered by the user.
- `Unit`: The unit of measurement for the product entered by the user.
- `Product`: The name of the product entered by the user.
- `UserToken`: The user token used for authentication.

### Methods

#### `Products(string token)`

Constructor method that initializes the page components and sets the user token.

#### `CreateAPIinstance()`

Asynchronously creates an instance of the `API` class by loading the API key from a file.

#### `LoadMauiAsset()`

Asynchronously loads the API key from the "APIKEY.env" file.

#### `CountEntry_TextChanged()`

Event handler for the count entry text changed event.

#### `UnitEntry_TextChanged()`

Event handler for the unit entry text changed event.

#### `ProductEntry_TextChanged()`

Event handler for the product entry text changed event.

#### `SubmitButton_Clicked()`

Event handler for the submit button click event. Adds a new product entry to the shopping list.
