module.exports = {
  async up(db, client) {
    const session = client.startSession();
    try {
      await session.withTransaction(async () => {
        const envName = process.env.ENVIRONMENT_NAME || "Local";

        console.log("------------------------");
        console.log("STARTING - Create Index Expenses.");
        console.log("Environment: ", envName);

        const collection = db.collection("expenses");

        await collection.createIndex({ id: 1 }, { unique: true });

        await collection.createIndex({ title: 1 });

        await collection.createIndex({ date: 1 });

        await collection.createIndex({ owner: 1 });

        console.log("FINISHED - Create Index Expenses.");
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
