const categories = [
  {
    id: "8ec5c9dd-a124-4ef0-8d4b-f6ab5a28544f",
    name: "Food & Dining",
    description:
      "Expenses related to meals, groceries, restaurants, and takeout.",
  },
  {
    id: "89fa7535-6efc-4075-9a79-09ac603d477f",
    name: "Transportation",
    description:
      "Expenses for public transit, fuel, car maintenance, and ridesharing services.",
  },
  {
    id: "fb3bfbf8-92c7-418a-84ee-a0394858cc03",
    name: "Housing",
    description: "Costs for rent, mortgage, utilities, and home maintenance.",
  },
  {
    id: "62ebad8f-8315-48e3-9142-83b0e951c80f",
    name: "Health & Fitness",
    description:
      "Spending on gym memberships, health insurance, medical bills, and wellness activities.",
  },
  {
    id: "f497de9d-5c11-4644-8e96-da507e5f091b",
    name: "Entertainment",
    description:
      "Expenditures for movies, concerts, games, subscriptions, and hobbies.",
  },
  {
    id: "73a00143-cd25-459d-b10c-90bfa8e09aa2",
    name: "Travel",
    description:
      "Costs for flights, accommodations, car rentals, and vacation activities.",
  },
  {
    id: "2c14e2da-ac72-4067-ba8f-13c3d131823a",
    name: "Shopping",
    description:
      "Expenses for clothing, electronics, and general retail shopping.",
  },
  {
    id: "ee048ea9-ec9f-494a-86ea-07599c4244d7",
    name: "Education",
    description: "Spending on tuition, books, courses, and school supplies.",
  },
  {
    id: "06f4449f-cb19-4c42-90f9-2f6c281443d1",
    name: "Savings & Investments",
    description:
      "Allocations for savings accounts, investments, and retirement funds.",
  },
  {
    id: "53ec0339-e6ce-499e-b569-a63bddc823f0",
    name: "Miscellaneous",
    description: "Uncategorized expenses or one-time purchases.",
  },
];

module.exports = {
  async up(db, client) {
    const session = client.startSession();
    try {
      await session.withTransaction(async () => {
        const envName = process.env.ENVIRONMENT_NAME || "Local";

        console.log("------------------------");
        console.log("STARTING - Seed Categories.");
        console.log("Environment: ", envName);

        console.log(`categories length: ${categories.length}`);
        const collection = db.collection("categories");

        await collection.deleteMany({});

        const options = { ordered: true };
        result = await collection.insertMany(categories, options);

        console.log(`${result.insertedCount} categories inserted.`);

        console.log("FINISHED - Seed Categories.");
        console.log("------------------------");
        console.log("");
      });
    } catch (error) {
      console.log(error);
      session.abortTransaction();
    } finally {
      await session.endSession();
    }
  },

  async down(db, client) {},
};
