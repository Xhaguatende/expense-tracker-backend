const currencies = [
  {
    name: "U.S. dollar",
    isoSymbol: "USD",
  },
  {
    name: "Euro",
    isoSymbol: "EUR",
  },
  {
    name: "Japanese yen",
    isoSymbol: "JPY",
  },
  {
    name: "Sterling",
    isoSymbol: "GBP",
  },
  {
    name: "Renminbi",
    isoSymbol: "CNY",
  },
  {
    name: "Australian dollar",
    isoSymbol: "AUD",
  },
  {
    name: "Canadian dollar",
    isoSymbol: "CAD",
  },
  {
    name: "Swiss franc",
    isoSymbol: "CHF",
  },
  {
    name: "Hong Kong dollar",
    isoSymbol: "HKD",
  },
  {
    name: "Singapore dollar",
    isoSymbol: "SGD",
  },
  {
    name: "Swedish krona",
    isoSymbol: "SEK",
  },
  {
    name: "South Korean won",
    isoSymbol: "KRW",
  },
  {
    name: "Norwegian krone",
    isoSymbol: "NOK",
  },
  {
    name: "New Zealand dollar",
    isoSymbol: "NZD",
  },
  {
    name: "Indian rupee",
    isoSymbol: "INR",
  },
  {
    name: "Mexican peso",
    isoSymbol: "MXN",
  },
  {
    name: "New Taiwan dollar",
    isoSymbol: "TWD",
  },
  {
    name: "South African rand",
    isoSymbol: "ZAR",
  },
  {
    name: "Brazilian real",
    isoSymbol: "BRL",
  },
  {
    name: "Danish krone",
    isoSymbol: "DKK",
  },
  {
    name: "Polish złoty",
    isoSymbol: "PLN",
  },
  {
    name: "Thai baht",
    isoSymbol: "THB",
  },
];

module.exports = {
  async up(db, client) {
    const session = client.startSession();
    try {
      await session.withTransaction(async () => {
        const envName = process.env.ENVIRONMENT_NAME || "Local";

        console.log("------------------------");
        console.log("STARTING - Seed Currencies.");
        console.log("Environment: ", envName);

        console.log(`Currencies length: ${currencies.length}`);
        const collection = db.collection("currencies");

        await collection.deleteMany({});

        const options = { ordered: true };
        result = await collection.insertMany(currencies, options);

        console.log(`${result.insertedCount} currencies inserted.`);

        console.log("FINISHED - Seed Currencies.");
        console.log("------------------------");
        console.log("");
      });

      await session.endSession();
    } catch (error) {
      console.log(error);
      session.abortTransaction();
    }
  },

  async down(db, client) {},
};
