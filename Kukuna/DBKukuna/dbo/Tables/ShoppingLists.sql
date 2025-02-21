CREATE TABLE [dbo].[ShoppingLists] (
    [ShoppingListID] INT             IDENTITY (1, 1) NOT NULL,
    [IngredientID]   INT             NULL,
    [UnitID]         INT             NULL,
    [TotalQuantity]  DECIMAL (10, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([ShoppingListID] ASC),
    FOREIGN KEY ([IngredientID]) REFERENCES [dbo].[Ingredients] ([IngredientID]),
    FOREIGN KEY ([UnitID]) REFERENCES [dbo].[Units] ([UnitID])
);

