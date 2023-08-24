-- The table Random was created using Wizard

USE Performance;

-- Checks whether the stored procedure SelectNumber exists or not, and drops it if it exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('SelectNumber'))
    DROP PROCEDURE SelectNumber;
GO

CREATE PROCEDURE SelectNumber
	@Number int
AS
	SELECT * 
	FROM Random 
	WHERE RandomNumber = @Number;
GO

-- Checks whether the stored procedure SelectSortedNumbers exists or not, and drops it if it exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('SelectSortedNumbers'))
    DROP PROCEDURE SelectSortedNumbers;
GO

CREATE PROCEDURE SelectSortedNumbers
AS
	SELECT RandomNumber, COUNT(RandomNumber) AS 'Counts'
	FROM Random 
	GROUP BY RandomNumber
	ORDER BY RandomNumber;
GO

-- Checks whether the stored procedure SelectAscendingSortedNumbersCounted exists or not, and drops it if it exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('SelectAscendingSortedNumbersCounted'))
    DROP PROCEDURE SelectAscendingSortedNumbersCounted;
GO

CREATE PROCEDURE SelectAscendingSortedNumbersCounted
AS
	SELECT RandomNumber, COUNT(RandomNumber) AS 'Counts'
	FROM Random 
	GROUP BY RandomNumber
	ORDER BY COUNT(RandomNumber);
GO

-- Checks whether the stored procedure SelectDescendingSortedNumbersCounted exists or not, and drops it if it exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('SelectDescendingSortedNumbersCounted'))
    DROP PROCEDURE SelectDescendingSortedNumbersCounted;
GO

CREATE PROCEDURE SelectDescendingSortedNumbersCounted
AS
	SELECT RandomNumber, COUNT(RandomNumber) AS 'Counted'
	FROM Random 
	GROUP BY RandomNumber
	ORDER BY COUNT(RandomNumber) DESC;
GO

-- Checks whether the stored procedure SelectRarestNumber exists or not, and drops it if it exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('SelectRarestNumber'))
    DROP PROCEDURE SelectRarestNumber;
GO

CREATE PROCEDURE SelectRarestNumber
AS
	SELECT TOP 1 RandomNumber AS 'Rarest', COUNT(RandomNumber) AS 'Counts'
	FROM Random 
	GROUP BY RandomNumber
	ORDER BY COUNT(RandomNumber);
GO

-- Checks whether the stored procedure SelectMostFrequentNumber exists or not, and drops it if it exist
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('SelectMostFrequentNumber'))
    DROP PROCEDURE SelectMostFrequentNumber;
GO

CREATE PROCEDURE SelectMostFrequentNumber
AS
	SELECT TOP 1 RandomNumber AS 'Frequently', COUNT(RandomNumber) AS 'Counts'
	FROM Random 
	GROUP BY RandomNumber
	ORDER BY COUNT(RandomNumber) DESC;
GO

-- Is commented because its already created
--CREATE INDEX index_RandomNumber
--ON Random (RandomNumber);

-- Assignment A) Search after a certain random number
EXEC SelectNumber @Number = 4711;

-- Assignment B) Select all numbers and sort them
EXEC SelectSortedNumbers;

-- Assignment C) Find how many times the rarest numbers or number occurs
EXEC SelectAscendingSortedNumbersCounted;

-- Assignment D) Find how many times the most frequent numbers or number occurs
EXEC SelectDescendingSortedNumbersCounted;

-- Assignment E) Use the last two results to find the most frequest and the rarest number
-- Either i can use my stored procedures i made for it, or just use the 
-- SelectAscendingSortedNumbersCounted and SelectDescendingSortedNumbersCounted to find the result
-- I used my stored procedures
EXEC SelectRarestNumber;
EXEC SelectMostFrequentNumber;
