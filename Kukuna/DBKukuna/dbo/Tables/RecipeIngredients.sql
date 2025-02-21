CREATE TABLE [dbo].[RecipeIngredients] (
    [RecipeIngredientID] INT             IDENTITY (1, 1) NOT NULL,
    [RecipeID]           INT             NULL,
    [IngredientID]       INT             NULL,
    [UnitID]             INT             NULL,
    [Quantity]           DECIMAL (10, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([RecipeIngredientID] ASC),
    FOREIGN KEY ([IngredientID]) REFERENCES [dbo].[Ingredients] ([IngredientID]),
    FOREIGN KEY ([RecipeID]) REFERENCES [dbo].[Recipes] ([RecipeID]),
    FOREIGN KEY ([UnitID]) REFERENCES [dbo].[Units] ([UnitID])
);

