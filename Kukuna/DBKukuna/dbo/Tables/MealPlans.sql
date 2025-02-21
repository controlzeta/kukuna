CREATE TABLE [dbo].[MealPlans] (
    [MealPlanID] INT  IDENTITY (1, 1) NOT NULL,
    [DayOfWeek]  DATE NOT NULL,
    [RecipeID]   INT  NULL,
    CONSTRAINT [PK__MealPlan__0620DB56CAE9CA86] PRIMARY KEY CLUSTERED ([MealPlanID] ASC),
    CONSTRAINT [FK__MealPlans__Recip__2F10007B] FOREIGN KEY ([RecipeID]) REFERENCES [dbo].[Recipes] ([RecipeID])
);

