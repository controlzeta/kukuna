CREATE TABLE [dbo].[Ingredients] (
    [IngredientID]   INT           IDENTITY (1, 1) NOT NULL,
    [IngredientName] VARCHAR (255) NOT NULL,
    [Category]       NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([IngredientID] ASC)
);

