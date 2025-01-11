module.exports = {
  async up(db, client) {
    const session = client.startSession();
    try {
      await session.withTransaction(async () => {
        const envName = process.env.ENVIRONMENT_NAME || "Local";

        console.log("------------------------");
        console.log("STARTING - Create ExpensesVew.");
        console.log("Environment: ", envName);

        await db.collection("expensesView").drop();

        await db.createCollection("expensesView", {
          viewOn: "expenses", // Source collection
          pipeline: [
            {
              $lookup: {
                from: "categories", // Collection to join
                localField: "categoryId", // Field from the `expenses` collection
                foreignField: "id", // Field from the `categories` collection
                as: "categoryDetails", // Alias for the joined data
              },
            },
            {
              $unwind: {
                path: "$categoryDetails",
                preserveNullAndEmptyArrays: true,
              },
            },
            {
              $project: {
                _id: 0, // Exclude `_id` from the output
                id: 1,
                title: 1,
                amount: "$amount.value",
                currencySymbol: "$amount.currencyIsoSymbol",
                date: 1,
                categoryId: 1,
                category: "$categoryDetails.name",
                owner: 1,
              },
            },
          ],
          collation: { locale: "en", strength: 2 }, // Case-insensitive collation
        });

        console.log("FINISHED - Create ExpensesView.");
        console.log("------------------------");
        console.log("");

        await session.endSession();
      });
    } catch (error) {
      console.log(error);
    }
  },

  async down(db, client) {},
};
