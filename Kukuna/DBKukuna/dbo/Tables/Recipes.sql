CREATE TABLE [dbo].[Recipes] (
    [RecipeID]     INT           IDENTITY (1, 1) NOT NULL,
    [RecipeName]   VARCHAR (255) NOT NULL,
    [Instructions] TEXT          NULL,
    [Servings]     INT           NULL,
    PRIMARY KEY CLUSTERED ([RecipeID] ASC)
);

