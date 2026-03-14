# Domain Layer Diagrams

This document contains **domain layer diagrams** for the ModularAuth project, illustrating the relationships between core classes: `Result`, `Result<T>`, `Error`, and `Guard`.

---

## 1️⃣ Result Pattern

```mermaid
classDiagram
class Result {
    +bool IsSuccess
    +bool IsFailure
    +Error? Error
    +static Success()
    +static Failure(Error)
}

class ResultT~T~ {
    +T Value
    +static Success(T)
    +static Failure(Error)
}

ResultT --> Result