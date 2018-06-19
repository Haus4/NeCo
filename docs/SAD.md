# Software Architecture Document

# Table of Contents
- [Introduction](#1-introduction)
    - [Purpose](#11-purpose)
    - [Scope](#12-scope)
    - [Definitions, Acronyms and Abbreviations](#13-definitions-acronyms-and-abbreviations)
    - [References](#14-references)
    - [Overview](#15-overview)
- [Architectural Representation](#2-architectural-representation)
- [Architectural Goals and Constraints](#3-architectural-goals-and-constraints)
- [Use-Case View](#4-use-case-view)
    - [Use-Case Realizations](#41-use-case-realizations)
- [Logical View](#5-logical-view)
    - [Overview](#51-overview)
    - [Architecturally Significant Design Packages](#52-architecturally-significant-design-packages)
    - [Pattern](#53-Pattern)
- [Process View](#6-process-view)
- [Deployment View](#7-deployment-view)
- [Implementation View](#8-implementation-view)
    - [Overview](#81-overview)
    - [Layers](#82-layers)
- [Data View](#9-data-view)
- [Size and Performance](#10-size-and-performance)
- [Quality](#11-quality)

## 1. Introduction
### 1.1 Purpose
This document provides a quick architectural overview of the system. It is intended to capture the significant architectural decisions which have been made on the system.

### 1.2 Scope
This document describes the architecture of the NeCo Project.

### 1.3 Definitions, Acronyms and Abbreviations
|			Abbreviation									|	Explanation		|
|---------------------------------------------------|---------------|
| MVVM | Model-View-ViewModel-Architecture |

### 1.4 References
n/a
### 1.5 Overview
The architectural details will be described in the following sections. This includes the class diagrams which gives an overview about the whole project.
## 2. Architectural Representation
As we develop a Chross-Plattform mobile ab MVVM is the pattern of choice.
It allows us to decouple the UI (View) from the Business Logic (Model/ViewModel).

Xamarin provides MVVM for our purposes.

![mvvm]
## 3. Architectural Goals and Constraints
As we use Xamarin we don't have any MVC tool.
## 4. Use-Case View
### 4.1 Use-Case Realizations

![oucd2]
## 5. Logical View
### 5.1 Overview
n/a
### 5.2 Architecturally Significant Design Packages
The class diagram; containing all Data Access Objects, Models and Controllers that we will need to finish for the basic functionality:

![Class Diagram]

### 5.3 Pattern
We’ve choosen a service locator pattern to be used on our server. This pattern is typical for C#.

The service locator pattern is a design pattern used in to encapsulate the processes involved in obtaining a service with a strong abstraction layer. 

This pattern uses a central registry known as the "service locator", which on request returns the information necessary to perform a certain task.

The approach simplifies component-based applications where all dependencies are cleanly listed at the beginning of the whole application design, consequently making traditional dependency injection a more complex way of connecting objects.

Therefore we rebuild our InfrastructureInitializer to a SocketServerFactory.
![patternserver]

As we already used some patterns since the beginning of development, we decided not to compromise the code integrity completely. Therefore not realy much changed in our code.

![patternclass]
## 6. Process View
n/a
## 7. Deployment View
![client/server]
## 8. Implementation View
### 8.1 Overview
n/a
### 8.2 Layers
n/a
## 9. Data View
n/a
## 10. Size and Performance
n/a
## 11. Quality
n/a


[Class Diagram]: https://github.com/Haus4/NeCo/raw/develop/docs/img/ClassDiagram_mvvm_2ndSem.png "Class Diagram with MVVM"

[mvvm]: https://github.com/Haus4/NeCo/raw/develop/docs/img/mvvm.png "Model-View-Viewmodel"

[client/server]: https://github.com/Haus4/NeCo/raw/develop/docs/img/Client_Server_Architecture.jpg "Client-Server-Architecture"

[oucd]: https://raw.githubusercontent.com/Haus4/NeCo/develop/docs/img/UseCaseDiagramm.jpg "Overall Use Case Diagram Semester 1"

[oucd2]: https://github.com/Haus4/NeCo/raw/develop/docs/img/UseCaseDiagramm_2nd.jpg "Overall Use Case Diagram Semester 2"

[patternclass]: https://raw.githubusercontent.com/Haus4/NeCo/develop/docs/img/before_server_cd.PNG "Image of Pattern Class Diagram"

[patternserver]: https://raw.githubusercontent.com/Haus4/NeCo/develop/docs/img/server_patterns.png "Image of Pattern Diagram"