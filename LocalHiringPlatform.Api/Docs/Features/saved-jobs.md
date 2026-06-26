
# Feature: Saved Jobs

## Business Purpose

Allows a candidate to save interesting jobs for future reference without applying immediately.

---

# React Flow

**Page:** `SavedJobsPage.tsx`

**Service:** `SavedJobService.ts`

**Type:** `Job`

## Data Flow

1. `SavedJobsPage.tsx` loads.
2. `useEffect()` calls `getMySavedJobs()`.
3. `SavedJobService` invokes the Saved Jobs API.
4. The API response is stored in the `jobs` state.
5. The `jobs` state is used to render the list of saved jobs.

---

# API Flow

**Controller:** `SavedJobController`

**Service:** `SavedJobService`

**Repository:** `SavedJobRepository`

## Processing Flow

1. Controller receives the request.
2. User Id is obtained from the JWT token.
3. Service performs business validations (if any).
4. Repository retrieves the saved jobs from the database.
5. Results are returned to the Service.
6. Service maps Entities to Models.
7. Controller maps Models to DTOs.
8. DTOs are returned to the React application.

---

# Database Flow

## Tables Involved

* Users
* CandidateProfiles
* SavedJobs
* Jobs

The repository queries the **SavedJobs** table and joins it with the related **Jobs** table to return the candidate's saved jobs.

---

# Data Flow

```text
Database
    ↓
Repository (Entities)
    ↓
Service (Models)
    ↓
Controller (DTOs)
    ↓
React Service
    ↓
React Type (Job)
    ↓
SavedJobsPage
```

---

# Business Rules

* Only authenticated candidates can save jobs.
* A candidate cannot save the same job multiple times.
* Only the candidate's own saved jobs are returned.

---

# Review Checklist

* [ ] React Type matches API DTO.
* [ ] API DTO contains only required fields.
* [ ] Repository returns only required data.
* [ ] Business validations are implemented in the Service layer.
* [ ] Authorization is enforced using JWT.
* [ ] Repository Pattern is followed.
* [ ] No direct `DbContext` access from the Service layer.

---

# Review Status

**Reviewed On:** YYYY-MM-DD

**Status:**

* [ ] Not Reviewed
* [ ] Reviewed
* [ ] Needs Refactoring

## Issues Found

* None

## Future Improvements

* Apply from saved job page. **Highest value feature for candidates.**
* Search within saved jobs.
* Sort by saved date.
* Filter by location or salary.
* Show AI match score.
* Notify the candidate when a saved job is about to expire.
