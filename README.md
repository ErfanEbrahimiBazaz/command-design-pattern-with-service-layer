# Command Design Pattern Implementation

This project demonstrates a clean implementation of the Command design pattern in C# .NET 9, featuring dependency injection and a service layer architecture.

## Overview

The Command pattern encapsulates requests as objects, allowing you to parameterize clients with different requests, queue operations, and support undo functionality. This implementation focuses on a text editor scenario with write and delete operations.

## Architecture

### Core Components

#### 1. **Receivers** (`Business\Receivers\`)
- **`TextEditor`**: The receiver that performs the actual work
  - Maintains text content state
  - Provides `Write()` and `DeleteLastNCharacters()` operations
  - Registered as **Scoped** in DI container

#### 2. **Commands** (`Business\Commands\`)
- **`ICommand`**: Interface defining the command contract
  - `CanExecute()`: Validates if command can be executed
  - `Execute()`: Performs the operation
  - `Undo()`: Reverses the operation

- **`WriteTextCommand`**: Adds text to the editor
- **`DeleteLastNCharactersCommand`**: Removes specified number of characters

#### 3. **Invoker** (`Business\Invoker\`)
- **`CommandManager`**: Manages command execution and history
  - Executes commands through `ExecuteCommand()`
  - Maintains command history for undo/redo functionality
  - Registered as **Scoped** in DI container

#### 4. **Service Layer** (`Business\Services\`)
- **`EditorCommandService`**: Abstracts command creation and execution
  - Provides high-level operations (`Write()`, `DeleteLastNCharacters()`, `Undo()`, `Redo()`)
  - Creates command instances with appropriate parameters
  - Validates commands before execution
  - Registered as **Scoped** in DI container

## Dependency Injection Management

### What's Injected
The DI container manages the following services:

```csharp
services.AddScoped<TextEditor>();        // Receiver
services.AddScoped<CommandManager>();    // Invoker  
services.AddScoped<EditorCommandService>(); // Service Layer
```

### Why Commands Are NOT Injected

**Commands are intentionally NOT registered in the DI container** for several important reasons:

1. **Runtime Parameters**: Commands require specific runtime parameters (text content, character count) that cannot be predetermined at container configuration time.

2. **Transient Nature**: Each command represents a specific operation with unique data - they should be created fresh for each operation, not managed by the container.

3. **Avoiding Container Pollution**: Commands are short-lived objects that don't benefit from lifetime management or dependency resolution.

4. **Flexibility**: Creating commands manually allows for dynamic parameter passing based on user input or business logic.

Example of why DI registration would fail:
```csharp
// This WOULD NOT WORK - container doesn't know what "Hello, " should be
services.AddTransient<ICommand, WriteTextCommand>();

// Instead, we create commands with specific parameters:
var command = new WriteTextCommand(textEditor, "Hello, ");
```

## Benefits of This Architecture

### 1. **Separation of Concerns**
- **TextEditor**: Focuses solely on text manipulation
- **Commands**: Encapsulate specific operations with undo capability
- **CommandManager**: Handles execution flow and history
- **EditorCommandService**: Provides clean API abstraction

### 2. **Dependency Injection Integration**
- Core services (TextEditor, CommandManager) are properly managed by DI
- Service layer provides clean abstraction over command complexity
- Testable architecture with mockable dependencies

### 3. **Command Pattern Advantages**
- **Undo/Redo Support**: Full operation history with bidirectional navigation
- **Command Validation**: `CanExecute()` prevents invalid operations
- **Extensibility**: Easy to add new command types
- **Decoupling**: Invoker doesn't need to know receiver implementation details

### 4. **Clean API**
The service layer provides an intuitive interface:
```csharp
editorCommandService.Write("Hello, ");
editorCommandService.DeleteLastNCharacters(5);
editorCommandService.Undo();
editorCommandService.Redo();
```

## Usage Example

```csharp
// Setup DI container
IServiceCollection services = new ServiceCollection();
services.AddCommandPatternDI();
var provider = services.BuildServiceProvider();

// Get service and use it
var editorService = provider.GetRequiredService<EditorCommandService>();

editorService.Write("Hello, World!");
editorService.DeleteLastNCharacters(7);  // Removes "World!"
editorService.Undo();                    // Restores "World!"
editorService.Undo();                    // Removes "Hello, "
```

## Key Design Decisions

1. **Service Layer**: Abstracts command complexity from consumers
2. **Scoped Lifetimes**: Appropriate for request-scoped operations
3. **Manual Command Creation**: Provides flexibility for runtime parameters
4. **Validation**: Commands validate themselves before execution
5. **State Management**: Commands store necessary state for undo operations

This implementation demonstrates how the Command pattern can be effectively integrated with modern .NET dependency injection while maintaining clean separation of concerns and providing robust undo/redo functionality.