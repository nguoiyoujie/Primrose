[![Build Status](https://dev.azure.com/yjnguoi/Primrose/_apis/build/status/Primrose?branchName=master)](https://dev.azure.com/yjnguoi/Primrose/_build/latest?definitionId=1&branchName=master)

![Primrose](doc/img/banner.png)

## About this Project
This is a set of primitive libraries for various functions used for my other projects. These functions are largely for organization and consistency purposes, with possible expansion to serve particular features.

This project should be treated as a constant work in-progress. Each project should embed its own compatible build of Primrose.

## Features

This project is still in the early stages, so content is still in the process of building up. The current stage of the project provides these options

- Collection types for ThreadSafeList, ThreadSafeDictionary, CircularQueue, Cache, generic Registry and ObjectPool

- A StateMachine

- Light-weight variable and basic arthimetric support for multi-element vectors structs such as float2, float3, float4.

- Generic container structs for variable pairs, triplets, and quadruplets.

- Method extensions for several existing types.

- ScopeCounter to assist in ensuring thread-safety in multi-threaded applications.

- Basic support for reading and writing an INI configuration type file

- An interpretive Expression and script engine, loosely based on C programming syntax.


## Possible Future Work

Integration with features from older work, such as:

- Binding variable changes to events.

- Actions, Conditions, Triggers

- Tasks and async methods

- Baseline error handling mechanisms

- Baseline logging and file writing mechanisms

- Application extension to SFML


## Prerequisites

This project is created and maintained with Visual Studio 2015.

The project is built for Microsoft .NET Framework v4.0.

