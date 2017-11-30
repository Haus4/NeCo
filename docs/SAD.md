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

![mvvm]
## 3. Architectural Goals and Constraints
As we use Xamarin we don't have any MVC tool.
## 4. Use-Case View
### 4.1 Use-Case Realizations
![oucd]
## 5. Logical View
### 5.1 Overview
n/a
### 5.2 Architecturally Significant Design Packages
The class diagram; containing all Data Access Objects, Models and Controllers that we will need to finish the basic functionality:

![Class Diagram]

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


[Class Diagram]: https://github.com/Haus4/NeCo/raw/develop/docs/img/ClassDiagram_mvvm.png "Class Diagram with MVVM"

[mvvm]: https://github.com/Haus4/NeCo/raw/develop/docs/img/mvvm.png "Model-View-Viewmodel"

[client/server]: https://github.com/Haus4/NeCo/raw/develop/docs/img/Client_Server_Architecture.jpg "Client-Server-Architecture"

[oucd]: https://raw.githubusercontent.com/Haus4/NeCo/develop/docs/img/UseCaseDiagramm.jpg "Overall Use Case Diagram"
