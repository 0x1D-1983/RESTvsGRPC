# REST VS gRPC Benchmark
This is an updated version of the code found in the article by Ruwan Fernando published on Medium [Evaluating Performance of REST vs. gRPC](https://medium.com/@EmperorRXF/evaluating-performance-of-rest-vs-grpc-1b8bdf0b22da)

The following changes have been applied:
- Recreate dotnet core 2.2 projects for NET 7
- Replace Newtonsoft with System.Text.Json
- Update obsolete BenchmarkDotNet function calls

## Rest API
In a new Terminal, navigate to the folder RestAPI and
```
dotnet run
```

## Grpc API
In a new Terminal, navigate to the folder GrpcAPI and
```
dotnet run
```

## Benchmark
In a new Terminal, navigate to the folder GrpcAPI and
```
dotnet build -c Release
dotnet run -c Release
```
