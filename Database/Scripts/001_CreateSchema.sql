/*
    School System database schema
    Generated from the structure of the working SchoolSystemDatabase.

    Safety:
    - Creates the database only when it does not exist.
    - Creates each table only when it does not exist.
    - Does not delete or update application data.
*/

IF DB_ID(N'SchoolSystemDatabase') IS NULL
BEGIN
    EXEC(N'CREATE DATABASE [SchoolSystemDatabase]');
END;
GO

USE [SchoolSystemDatabase];
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;
GO

IF OBJECT_ID(N'dbo.Countries', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Countries
    (
        CountryID   int IDENTITY(1, 1) NOT NULL,
        CountryName nvarchar(50) NOT NULL,
        CONSTRAINT PK_Countries PRIMARY KEY CLUSTERED (CountryID)
    );
END;
GO

IF OBJECT_ID(N'dbo.Stages', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Stages
    (
        StageID   int IDENTITY(1, 1) NOT NULL,
        StageName nvarchar(50) NOT NULL,
        StruEduID int NULL,
        IsActive  bit NOT NULL CONSTRAINT DF_Stages_IsActive DEFAULT (1),
        CONSTRAINT PK_Stages PRIMARY KEY CLUSTERED (StageID)
    );
END;
GO

IF OBJECT_ID(N'dbo.Grades', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Grades
    (
        GradeID   int IDENTITY(1, 1) NOT NULL,
        GradeName nvarchar(50) NOT NULL,
        StageID   int NOT NULL,
        IsActive  bit NOT NULL CONSTRAINT DF_Grades_IsActive DEFAULT (1),
        CONSTRAINT PK_Grades PRIMARY KEY CLUSTERED (GradeID),
        CONSTRAINT FK_Grades_Stages FOREIGN KEY (StageID)
            REFERENCES dbo.Stages (StageID)
    );
END;
GO

IF OBJECT_ID(N'dbo.Sections', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Sections
    (
        SectionID   int IDENTITY(1, 1) NOT NULL,
        SectionName nvarchar(50) NOT NULL,
        GradeID     int NOT NULL,
        IsActive    bit NOT NULL CONSTRAINT DF_Sections_IsActive DEFAULT (1),
        Notes       nvarchar(255) NULL,
        CONSTRAINT PK_Sections PRIMARY KEY CLUSTERED (SectionID),
        CONSTRAINT FK_Sections_Grades FOREIGN KEY (GradeID)
            REFERENCES dbo.Grades (GradeID)
    );
END;
GO

IF OBJECT_ID(N'dbo.Semesters', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Semesters
    (
        SemesterID   int IDENTITY(1, 1) NOT NULL,
        SemesterName nvarchar(50) NOT NULL,
        AcademicYear nvarchar(20) NOT NULL,
        StartDate    date NOT NULL,
        EndDate      date NOT NULL,
        IsActive     bit NOT NULL,
        CONSTRAINT PK_Semesters PRIMARY KEY CLUSTERED (SemesterID)
    );
END;
GO

IF OBJECT_ID(N'dbo.Persons', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Persons
    (
        PersonID       int IDENTITY(1, 1) NOT NULL,
        NationalNo     int NULL,
        FirstName      nvarchar(50) NOT NULL,
        MiddleName     nvarchar(50) NULL,
        LastName       nvarchar(50) NOT NULL,
        BirthDate      date NULL,
        Gender         int NULL,
        CountryID      int NULL,
        Phone          nvarchar(100) NULL,
        Email          nvarchar(100) NULL,
        PhoneSecondary nvarchar(20) NULL,
        Address        nvarchar(200) NULL,
        ImagePath      nvarchar(300) NULL,
        IsActive       bit NULL,
        CreatedAt      datetime2(7) NULL,
        UpdatedAt      datetime2(7) NULL,
        CONSTRAINT PK_Persons PRIMARY KEY CLUSTERED (PersonID)
    );
END;
GO

IF OBJECT_ID(N'dbo.Students', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Students
    (
        StudentID    int IDENTITY(1, 1) NOT NULL,
        PersonID     int NULL,
        SectionID    int NULL,
        EnrollmentNo nvarchar(50) NULL,
        GradeID      int NULL,
        AdmissionDate date NULL,
        IsActive     bit NULL,
        Notes        nvarchar(200) NULL,
        CreatedAt    datetime2(7) NULL,
        UpdatedAt    datetime2(7) NULL,
        CONSTRAINT PK_Students PRIMARY KEY CLUSTERED (StudentID),
        CONSTRAINT FK_Students_Persons FOREIGN KEY (PersonID)
            REFERENCES dbo.Persons (PersonID),
        CONSTRAINT FK_Students_Sections FOREIGN KEY (SectionID)
            REFERENCES dbo.Sections (SectionID)
    );
END;
GO

IF OBJECT_ID(N'dbo.Teachers', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Teachers
    (
        TeacherID     int IDENTITY(1, 1) NOT NULL,
        PersonID      int NOT NULL,
        EmployeeNo    nvarchar(30) NOT NULL,
        HireDate      date NOT NULL,
        Specialization nvarchar(100) NOT NULL,
        Qualification nvarchar(100) NOT NULL,
        Notes          nvarchar(200) NULL,
        IsActive       bit NOT NULL CONSTRAINT DF_Teachers_IsActive DEFAULT (1),
        CONSTRAINT PK_Teachers PRIMARY KEY CLUSTERED (TeacherID),
        CONSTRAINT FK_Teachers_Persons FOREIGN KEY (PersonID)
            REFERENCES dbo.Persons (PersonID)
            ON UPDATE CASCADE
    );
END;
GO

IF OBJECT_ID(N'dbo.Subjects', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Subjects
    (
        SubjectID      int IDENTITY(1, 1) NOT NULL,
        SubjectName    nvarchar(100) NOT NULL,
        SubjectCode    nvarchar(20) NOT NULL,
        Description    nvarchar(200) NULL,
        Credits        int NOT NULL,
        IsActive       bit NOT NULL CONSTRAINT DF_Subjects_IsActive DEFAULT (1),
        ParentSubjectID int NULL,
        ParentID        int NULL,
        CONSTRAINT PK_Subjects PRIMARY KEY CLUSTERED (SubjectID),
        CONSTRAINT FK_Subjects_Parent FOREIGN KEY (ParentSubjectID)
            REFERENCES dbo.Subjects (SubjectID),
        CONSTRAINT CHK_NoSelfParent CHECK
            (ParentID IS NULL OR ParentID <> SubjectID)
    );
END;
GO

IF OBJECT_ID(N'dbo.StudentSubjects', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.StudentSubjects
    (
        StudentSubjectID int IDENTITY(1, 1) NOT NULL,
        StudentID        int NOT NULL,
        SubjectID        int NOT NULL,
        SemesterID       int NOT NULL,
        SectionID        int NOT NULL,
        CONSTRAINT PK_StudentSubjects PRIMARY KEY CLUSTERED (StudentSubjectID),
        CONSTRAINT UQ_Student_Subject_Semester
            UNIQUE NONCLUSTERED (StudentID, SubjectID, SemesterID),
        CONSTRAINT FK_StudentSubjects_Students FOREIGN KEY (StudentID)
            REFERENCES dbo.Students (StudentID),
        CONSTRAINT FK_StudentSubjects_Subjects FOREIGN KEY (SubjectID)
            REFERENCES dbo.Subjects (SubjectID),
        CONSTRAINT FK_StudentSubjects_Semesters FOREIGN KEY (SemesterID)
            REFERENCES dbo.Semesters (SemesterID),
        CONSTRAINT FK_StudentSubjects_Sections FOREIGN KEY (SectionID)
            REFERENCES dbo.Sections (SectionID)
    );
END;
GO

IF OBJECT_ID(N'dbo.Pages', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Pages
    (
        PageID     int IDENTITY(1, 1) NOT NULL,
        PageNumber int NOT NULL,
        PagePath   nvarchar(300) NULL,
        CONSTRAINT PK_Pages PRIMARY KEY CLUSTERED (PageID)
    );
END;
GO

IF OBJECT_ID(N'dbo.QuranTracks', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.QuranTracks
    (
        QuranTrackID int IDENTITY(1, 1) NOT NULL,
        TrackName    nvarchar(100) NOT NULL,
        CONSTRAINT PK_QuranTracks PRIMARY KEY CLUSTERED (QuranTrackID),
        CONSTRAINT UQ_QuranTracks_TrackName UNIQUE NONCLUSTERED (TrackName)
    );
END;
GO

IF OBJECT_ID(N'dbo.Parts', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Parts
    (
        PartsID    int IDENTITY(1, 1) NOT NULL,
        SubjectID  int NOT NULL,
        PartNumber int NOT NULL,
        PartName   nvarchar(100) NULL,
        StartPage  int NOT NULL,
        EndPage    int NOT NULL,
        CONSTRAINT PK_Parts PRIMARY KEY CLUSTERED (PartsID),
        CONSTRAINT FK_Parts_Subjects FOREIGN KEY (SubjectID)
            REFERENCES dbo.Subjects (SubjectID)
    );
END;
GO

IF OBJECT_ID(N'dbo.StudentParts', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.StudentParts
    (
        StudentPartID int IDENTITY(1, 1) NOT NULL,
        StudentID     int NOT NULL,
        PartsID       int NOT NULL,
        QuranTrackID  int NOT NULL,
        IsArchived    bit NOT NULL CONSTRAINT DF_StudentParts_IsArchived DEFAULT (0),
        CONSTRAINT PK_StudentParts PRIMARY KEY CLUSTERED (StudentPartID),
        CONSTRAINT FK_StudentParts_Students FOREIGN KEY (StudentID)
            REFERENCES dbo.Students (StudentID),
        CONSTRAINT FK_StudentParts_Parts FOREIGN KEY (PartsID)
            REFERENCES dbo.Parts (PartsID)
    );

    CREATE UNIQUE NONCLUSTERED INDEX UX_StudentParts
        ON dbo.StudentParts (StudentID, PartsID, QuranTrackID);
END;
GO

IF OBJECT_ID(N'dbo.PageProgress', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.PageProgress
    (
        PageProgressID int IDENTITY(1, 1) NOT NULL,
        StudentPartID  int NULL,
        PageID          int NOT NULL,
        Status          int NOT NULL,
        RepeatCount     int NOT NULL CONSTRAINT DF_PageProgress_RepeatCount DEFAULT (0),
        CreatedAt       datetime2(0) NOT NULL CONSTRAINT DF_PageProgress_CreatedAt DEFAULT (GETDATE()),
        UpdatedAt       datetime2(0) NOT NULL CONSTRAINT DF_PageProgress_UpdatedAt DEFAULT (GETDATE()),
        CONSTRAINT PK_PageProgress PRIMARY KEY CLUSTERED (PageProgressID),
        CONSTRAINT FK_PageProgress_StudentParts FOREIGN KEY (StudentPartID)
            REFERENCES dbo.StudentParts (StudentPartID),
        CONSTRAINT FK_PageProgress_Pages FOREIGN KEY (PageID)
            REFERENCES dbo.Pages (PageID)
    );
END;
GO

IF OBJECT_ID(N'dbo.RecitationErrors', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.RecitationErrors
    (
        ErrorID        int IDENTITY(1, 1) NOT NULL,
        PageProgressID int NOT NULL,
        AyahNumber     int NOT NULL,
        ErrorType      tinyint NOT NULL,
        CONSTRAINT PK_RecitationErrors PRIMARY KEY CLUSTERED (ErrorID),
        CONSTRAINT FK_RecitationErrors_PageProgress FOREIGN KEY (PageProgressID)
            REFERENCES dbo.PageProgress (PageProgressID)
    );
END;
GO

IF OBJECT_ID(N'dbo.trg_CheckParentSubject', N'TR') IS NOT NULL
BEGIN
    DROP TRIGGER dbo.trg_CheckParentSubject;
END;
GO

CREATE TRIGGER dbo.trg_CheckParentSubject
ON dbo.StudentSubjects
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS
    (
        SELECT 1
        FROM inserted AS i
        INNER JOIN dbo.Subjects AS s ON i.SubjectID = s.SubjectID
        WHERE s.ParentSubjectID IS NOT NULL
          AND NOT EXISTS
          (
              SELECT 1
              FROM dbo.StudentSubjects AS ss
              WHERE ss.StudentID = i.StudentID
                AND ss.SubjectID = s.ParentSubjectID
          )
    )
    BEGIN
        RAISERROR
        (
            'Cannot assign child subject before parent subject',
            16,
            1
        );
        ROLLBACK TRANSACTION;
        RETURN;
    END;
END;
GO

PRINT N'SchoolSystemDatabase schema is ready.';
GO
