# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Architecture

This is a .NET 9 Blazor application using a shared component architecture:

- **ainbox.Shared** - Common Blazor components, pages, and services shared between web and MAUI
- **ainbox.Web** - ASP.NET Core Blazor Server web application
- **ainbox** - .NET MAUI cross-platform desktop/mobile application (currently Windows only)

### Key Architecture Patterns

- **Shared Services Pattern**: Both web and MAUI projects implement platform-specific versions of shared interfaces:
  - `IFormFactor` - Detects device type (Web/Mobile/Desktop)
  - `ISettingsService` - Handles platform-specific settings storage
- **Dependency Injection**: Both platforms register their specific service implementations in their respective startup files
- **Blazor Components**: UI components in ainbox.Shared are reused across both web and MAUI platforms

### External Dependencies

- **MailKit** (4.10.0) - Email handling library used in the shared project
- **Microsoft.AspNetCore.Components.WebView.Maui** - Enables Blazor in MAUI
- **Xamarin.Essentials** - Cross-platform APIs for both web and MAUI projects

## Common Commands

### Build and Run
```bash
# Build entire solution
dotnet build

# Run web application (development)
dotnet run --project ainbox/ainbox.Web

# Run MAUI application (Windows)
dotnet run --project ainbox/ainbox
```

### Development URLs
- Web application: https://localhost:7123 (HTTPS) or http://localhost:5286 (HTTP)
- MAUI runs as native Windows application

### Project Structure Navigation
- Web-specific code: `ainbox/ainbox.Web/`
- MAUI-specific code: `ainbox/ainbox/`
- Shared components and pages: `ainbox/ainbox.Shared/`
- Solution file: `ainbox.sln`

## Code Style

The project uses comprehensive EditorConfig settings with:
- 4-space indentation
- C# 9+ language features with nullable reference types enabled
- Pascal case naming conventions
- Expression-bodied members for accessors and properties
- Braces required for all code blocks
