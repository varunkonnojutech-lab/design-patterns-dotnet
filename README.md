# Design Patterns in .NET 8

[![Build](https://github.com/varunkonnojutech-lab/design-patterns-dotnet/actions/workflows/ci.yml/badge.svg)](https://github.com/varunkonnojutech-lab/design-patterns-dotnet/actions/workflows/ci.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4)](https://dotnet.microsoft.com/)
[![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](https://codespaces.new/varunkonnojutech-lab/design-patterns-dotnet)

A hands-on catalog of the classic Gang of Four design patterns, implemented in modern C# 12 / .NET 8. Every pattern lives in its own file with a runnable `Demo()`, so you can read the code and watch it work.

## Who is this for

Developers who know C# basics and want to recognize — and correctly apply — the patterns that show up in real codebases and interviews.

## What you'll learn

- The 9 most-used GoF patterns, grouped by intent: creational, structural, behavioral
- How each pattern looks in *modern* C# (`Lazy<T>`, records, pattern matching) — not 1990s Java translated line-by-line
- When to reach for a pattern — and when a simpler design wins
- How to verify pattern behavior with xUnit tests

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) — or click **Open in GitHub Codespaces** above for a ready-made environment in your browser, no install needed.

## Quick start

```bash
git clone https://github.com/varunkonnojutech-lab/design-patterns-dotnet.git
cd design-patterns-dotnet
dotnet run --project src/DesignPatterns   # runs all 9 demos
dotnet test                                # runs the xUnit test suite
```

## How it works

Each pattern is self-contained under `src/DesignPatterns/Patterns/<Type>/<Pattern>/` with a `Demo()` method. `Program.cs` runs all nine demos with section headers, so one command shows you every pattern in action. `tests/DesignPatterns.Tests` pins down the key behaviors (singleton identity, decorator caching, observer notify/unsubscribe, command undo, …).

| Pattern | Type | Intent |
|---|---|---|
| Singleton | Creational | Ensure a class has exactly one instance (`Lazy<T>`) |
| Factory Method | Creational | Let subclasses decide which payment processor to create |
| Builder | Creational | Build an immutable HTTP request step by step with a fluent API |
| Adapter | Structural | Wrap a legacy logger behind a modern `ILogger` interface |
| Decorator | Structural | Add caching around a slow data service without changing it |
| Facade | Structural | One simple `PlaceOrder` API over inventory, payment, and shipping |
| Strategy | Behavioral | Swap discount algorithms at runtime |
| Observer | Behavioral | Notify subscribers when a stock price changes |
| Command | Behavioral | Encapsulate light on/off actions with an undo stack |

## Why this repo

Reading about patterns is not enough — you need to see each one solve a concrete problem in code you can step through, change, and break. This repo keeps every pattern small, self-contained, and covered by tests, so it works as a reference you can come back to whenever you spot a pattern-shaped problem in real code.

## Project structure

```
design-patterns-dotnet/
├── src/DesignPatterns/
│   ├── Program.cs                  # Runs all 9 demos
│   └── Patterns/
│       ├── Creational/
│       │   ├── Singleton/          # Lazy<T> app settings
│       │   ├── FactoryMethod/      # Payment processors
│       │   └── Builder/            # Fluent HTTP request builder
│       ├── Structural/
│       │   ├── Adapter/            # Legacy logger → ILogger
│       │   ├── Decorator/          # Caching over IDataService
│       │   └── Facade/             # Order orchestration
│       └── Behavioral/
│           ├── Strategy/           # Discount strategies
│           ├── Observer/           # Stock ticker notifications
│           └── Command/            # Remote control with undo
├── tests/DesignPatterns.Tests/     # xUnit tests for each pattern
├── .devcontainer/                  # One-click GitHub Codespaces setup
├── .github/workflows/ci.yml        # CI: build + test on push
└── LICENSE
```

## Next steps

Ready to see patterns in a real system? Continue with [aspnet-core-webapi-starter](https://github.com/varunkonnojutech-lab/aspnet-core-webapi-starter) — a production-style REST API that puts these ideas to work.

## License

MIT — see [LICENSE](LICENSE).
