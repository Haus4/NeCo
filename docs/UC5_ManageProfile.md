# NeCo - Use-Case Specification: Manage Profile

## 1. Use-Case Name
Manage Profile

### 1.1 Brief Description
The app provides the user with the possibility to change his profile information.

### 1.2 Screenshot Mockup

![][screenshot]


## 2. Flow of Events

### 2.1 Basic Flow

![][ucd]


The `.feature`-file can be found [here][gherkin file].

### 2.2 Alternative Flow
(n/a)


## 3. Special Requirements
### 3.1 Use chat protocol: TCP
The Android-App and the Xamarin-Backend should transfer their data with the Transmission Control Protocol (TCP). 

### 3.1. Use encryption library: Signal Protocol
The client uses the encryption library to encrypt the message.


## 4. Preconditions

### 4.1 User key is generated
The chat messages are signed with a PGP-Key, which is generated upon opening the app.

### 4.2 User key is exchanged
The keys must have been exchanged before chating.

### 4.3 User is on the main page
The user should have already opened the NeCo Application on his smartphone and navigated to the main page.


## 5. Postconditions
(n/a)


## 6. Extension Points
(n/a)

## 7. Function Point calculation
(n/a)
<!--
This use case was estimated with 24 FPs. See the table and screenshot below for details:

| Transaction | DET's | RET's | FTR's | Complexity |
|-----------------------|:-:|:-:|:-:|:---:|
| EI                    | 1 | - | 0 | Low |
| EO                    | 0 | - | 3 | Low |
| ILF User              | 12 | 0 | - | Low |
| ILF Media             | 17 | 0 | - | Low |
| ILF Video             | 1 | 6 | - | Average |
| EIF                   | - | - | - | - |

![][fp calculation]

All function point calculation tables are also located in one spreadsheet. Please take a look at this [document][fpc spreadsheet].

-->

<!-- Link definitions: 
[basic flow]: <link> "Use Case Diagram: Chat"
-->
[screenshot]: <link> "Manage Profile Mockup"

[ucd]: <link> "Feature description in Gherkin"

[gherkin file]: https://github.com/Haus4/NeCo/blob/develop/docs/UC5.feature ".feature file"
<!--
[fp calculation]: <link> "FP calculation"
[fpc spreadsheet]:<link> "Function point calculation spreadsheet"

-->
