# Simple Code Generator for C++ classes

A very basic class generator for C++ classes. See `--help` for instructions.

# Install and run

Note: this is how I run the generator. You should be able to use every `dotnet build/run/publish` command you require for your use case.

- `dotnet publish -c Release -o ~/bin/class-generator`
- `dotnet ~/bin/class-generator/ClassGenerator.dll [params]`