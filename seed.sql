USE EmployeeApiDb;

-- Drop table if it exists
IF OBJECT_ID('dbo.Employees','U') IS NOT NULL 
    DROP TABLE dbo.Employees;

-- Create table
CREATE TABLE dbo.Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(200) NOT NULL UNIQUE,
    Department NVARCHAR(100) NULL,
    DateOfJoining DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    IsActive BIT NOT NULL DEFAULT 1
);

-- Insert sample data
INSERT INTO dbo.Employees (FirstName, LastName, Email, Department, DateOfJoining, IsActive)
VALUES
('Aisha', 'Khan', 'aisha.khan@example.com', 'Engineering', '2020-01-15', 1),
('Manas', 'Nayak', 'manas.nayak@example.com', 'IT', '2021-06-10', 1),
('Sobhan', 'Patra', 'sobhan.patra@example.com', 'HR', '2022-03-05', 1),
('Riya', 'Sharma', 'riya.sharma@example.com', 'Marketing', '2023-01-20', 1),
('Parshu', 'Ram', 'parshu.doe@example.com', 'Finance', '2019-11-25', 0);
