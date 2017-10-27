# NeCo - Use-Case Specification: Chat

## 1. Use-Case Name
Chat

### 1.1 Brief Description
The app provides the user with the possibility to chat with another person.

### 1.2 Screenshot Mockup

![][screenshot]


## 2. Flow of Events

### 2.1 Basic Flow
<!-- ![][basic flow] -->
![][ucd]

<!--
The `.feature`-file can be found [here][gherkin file].
-->
### 2.2 Alternative Flow
(n/a)


## 3. Special Requirements
### 3.1 Use chat protocol: TCP
The Android-App and the Xamarin-Backend should transfer their data with the Transmission Control Protocol (TCP). 


## 4. Preconditions

### 4.1 User key is generated
The chat messages are signed with a PGP-Key, which is generated upon opening the app.

### 4.2 User key is exchanged
The keys must have been exchanged before chating.

### 4.3 User on the main page
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

<!-- Link definitions: -->
[basic flow]: https://github.com/Haus4/NeCo/blob/master/docs/img/UC1_Chat_UCD "Use Case Diagram: Chat"

[screenshot]: https://github.com/Haus4/NeCo/raw/master/docs/img/UC1_Chat_Mockup.png "Chat Mockup"

[ucd]: https://github.com/Haus4/NeCo/blob/master/docs/img/UC1_Chat_UCD_f.jpg "Feature description in Gherkin"
<!--
[gherkin file]: <link> ".feature file"

[fp calculation]: <link> "FP calculation"
[fpc spreadsheet]:<link> "Function point calculation spreadsheet"

-->
