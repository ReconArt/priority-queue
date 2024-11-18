# ReconArt.PriorityQueue

## Overview

`ReconArt.PriorityQueue` is a .NET library that provides a fast and efficient implementation of a priority queue with constant time complexity for all operations. It supports only 6 priorities, making it suitable for scenarios where a limited number of priority levels are sufficient.

## Features

- Constant time complexity for all operations
- Supports 6 priority levels
- Simple API for enqueueing, dequeueing, and removing elements
- Designed for scenarios where a limited number of priority levels are sufficient
- Supports .NET 8.0 and .NET 9.0

## Installation

To install the `ReconArt.PriorityQueue` package, use the NuGet Package Manager or the Package Manager Console with the following command:

```powershell
Install-Package ReconArt.PriorityQueue
```

## Usage

### Creating a Priority Queue

You can create a priority queue and add elements with specified priorities:

```csharp
var priorityQueue = new PriorityQueue<string>();

// Enqueue elements with priorities (0 to 5, where 0 is the highest priority)
priorityQueue.Enqueue("High Priority Task", 0);
priorityQueue.Enqueue("Medium Priority Task", 2);
priorityQueue.Enqueue("Low Priority Task", 5);
```

### Dequeueing Elements

Dequeue elements from the priority queue, which removes and returns the element with the highest priority:

```csharp
var task = priorityQueue.Dequeue();
// task will be "High Priority Task"
```

### Peeking at the Next Element

Peek at the next element without removing it:

```csharp
var nextTask = priorityQueue.Peek();
```

### Removing Specific Elements

Remove a specific element by its node and priority:

```csharp
var node = priorityQueue.Enqueue("Specific Task", 3);
bool removed = priorityQueue.TryRemove(node, 3);
```

## Limitations

- The priority queue is not thread-safe.
- Supports only 6 priority levels (0 to 5).

## Contributing

If you'd like to contribute to the project, please reach out to the [ReconArt/priority-queue](https://github.com/ReconArt/priority-queue) team.

## Support

If you encounter any issues or require assistance, please file an issue in the [GitHub Issues](https://github.com/ReconArt/priority-queue/issues) section of the repository.

## Authors and Acknowledgments

Developed by [ReconArt, Inc.](https://reconart.com/).
