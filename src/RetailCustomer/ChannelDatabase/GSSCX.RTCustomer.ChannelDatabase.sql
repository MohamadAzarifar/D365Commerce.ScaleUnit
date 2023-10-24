/**
 * SAMPLE CODE NOTICE
 * 
 * THIS SAMPLE CODE IS MADE AVAILABLE AS IS.  MICROSOFT MAKES NO WARRANTIES, WHETHER EXPRESS OR IMPLIED,
 * OF FITNESS FOR A PARTICULAR PURPOSE, OF ACCURACY OR COMPLETENESS OF RESPONSES, OF RESULTS, OR CONDITIONS OF MERCHANTABILITY.
 * THE ENTIRE RISK OF THE USE OR THE RESULTS FROM THE USE OF THIS SAMPLE CODE REMAINS WITH THE USER.
 * NO TECHNICAL SUPPORT IS PROVIDED.  YOU MAY NOT DISTRIBUTE THIS CODE UNLESS YOU HAVE A LICENSE AGREEMENT WITH MICROSOFT THAT ALLOWS YOU TO DO SO.
 */

 -- Create the extension table to store the custom fields.

IF (SELECT OBJECT_ID('[ext].[GSSCX_RTCustomerTABLE]')) IS NULL 
BEGIN
    CREATE TABLE
        [ext].[GSSCX_RTCustomerTABLE]
    (
        [RTCustomerID]     BIGINT IDENTITY(1,1) NOT NULL,
        [RTCustomerINT]    INT NOT NULL DEFAULT ((0)),
        [RTCustomerSTRING] NVARCHAR(64) NOT NULL DEFAULT (('')),
        CONSTRAINT [I_RTCustomerTABLE_RTCustomerID] PRIMARY KEY CLUSTERED 
        (
            [RTCustomerID] ASC
        ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]

    ALTER TABLE [ext].[GSSCX_RTCustomerTABLE] WITH CHECK ADD CHECK (([RTCustomerID]<>(0)))
END
GO

GRANT SELECT, INSERT, UPDATE, DELETE ON OBJECT::[ext].[GSSCX_RTCustomerTABLE] TO [DataSyncUsersRole]
GO

-- Create a stored procedure CRT can use to add entries to the custom table.

IF OBJECT_ID(N'[ext].[GSSCX_INSERTRTCustomer]', N'P') IS NOT NULL
    DROP PROCEDURE [ext].[GSSCX_INSERTRTCustomer]
GO

CREATE PROCEDURE [ext].[GSSCX_INSERTRTCustomer]
    @i_RTCustomerInt    INT,
    @s_RTCustomerString NVARCHAR(64)
AS
BEGIN
    SET NOCOUNT ON

    INSERT INTO
         [ext].[GSSCX_RTCustomerTABLE]
        (RTCustomerINT, RTCustomerSTRING)
    OUTPUT
        INSERTED.RTCustomerID
    VALUES
        (@i_RTCustomerInt, @s_RTCustomerString)
END;
GO

GRANT EXECUTE ON [ext].[GSSCX_INSERTRTCustomer] TO [UsersRole];
GO

GRANT EXECUTE ON [ext].[GSSCX_INSERTRTCustomer] TO [DeployExtensibilityRole];
GO

-- Create the custom view that can query a complete RTCustomer Entity.

IF (SELECT OBJECT_ID('[ext].[GSSCX_RTCustomerVIEW]')) IS NOT NULL
    DROP VIEW [ext].[GSSCX_RTCustomerVIEW]
GO

CREATE VIEW [ext].[GSSCX_RTCustomerVIEW] AS
(
    SELECT
        et.RTCustomerINT,
        et.RTCustomerSTRING,
        et.RTCustomerID
    FROM
        [ext].[GSSCX_RTCustomerTABLE] et
)
GO

GRANT SELECT ON OBJECT::[ext].[GSSCX_RTCustomerVIEW] TO [UsersRole];
GO

GRANT SELECT ON OBJECT::[ext].[GSSCX_RTCustomerVIEW] TO [DeployExtensibilityRole];
GO

-- Create a stored procedure CRT can use to perform updates.

IF OBJECT_ID(N'[ext].[GSSCX_UPDATERTCustomer]', N'P') IS NOT NULL
    DROP PROCEDURE [ext].[GSSCX_UPDATERTCustomer]
GO

CREATE PROCEDURE [ext].[GSSCX_UPDATERTCustomer]
    @bi_Id           BIGINT,
    @i_RTCustomerInt    INT,
    @s_RTCustomerString NVARCHAR(64)
AS
BEGIN
    SET NOCOUNT ON

    UPDATE
        [ext].[GSSCX_RTCustomerTABLE]
    SET
        RTCustomerINT = @i_RTCustomerInt,
        RTCustomerSTRING = @s_RTCustomerString
    WHERE
        RTCustomerID = @bi_Id
END;
GO

GRANT EXECUTE ON [ext].[GSSCX_UPDATERTCustomer] TO [UsersRole];
GO

GRANT EXECUTE ON [ext].[GSSCX_UPDATERTCustomer] TO [DeployExtensibilityRole];
GO

-- Create a stored procedure CRT can use to delete RTCustomer Entities.

IF OBJECT_ID(N'[ext].[GSSCX_DELETERTCustomer]', N'P') IS NOT NULL
    DROP PROCEDURE [ext].[GSSCX_DELETERTCustomer]
GO

CREATE PROCEDURE [ext].[GSSCX_DELETERTCustomer]
    @bi_Id           BIGINT
AS
BEGIN
    SET NOCOUNT ON

    DELETE FROM
        [ext].[GSSCX_RTCustomerTABLE]
    WHERE
        RTCustomerID = @bi_Id
END;
GO

GRANT EXECUTE ON [ext].[GSSCX_DELETERTCustomer] TO [UsersRole];
GO

GRANT EXECUTE ON [ext].[GSSCX_DELETERTCustomer] TO [DeployExtensibilityRole];
GO
