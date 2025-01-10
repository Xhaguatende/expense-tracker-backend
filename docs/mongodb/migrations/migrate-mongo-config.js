
var mongodbCS = process.env.MONGODBSETTINGS_CONNECTIONSTRING || "mongodb://localhost:27017/?directConnection=true";
var mongodbName = process.env.MONGODBSETTINGS_DATABASENAME || "expense-tracker";

const config = {
  mongodb: {
    url: mongodbCS,

    databaseName: mongodbName,

    options: {
      useNewUrlParser: true, 
      useUnifiedTopology: true, 
      connectTimeoutMS: 300000, // 5 minutes
      socketTimeoutMS: 300000, // 5 minutes
    }
  },

  // The migrations dir, can be an relative or absolute path. Only edit this when really necessary.
  migrationsDir: "migrations",

  // The mongodb collection where the applied changes are stored. Only edit this when really necessary.
  changelogCollectionName: "changelog",

  // The file extension to create migrations and search for in migration dir 
  migrationFileExtension: ".js",

  // Enable the algorithm to create a checksum of the file contents and use that in the comparison to determine
  // if the file should be run. Requires that scripts are coded to be run multiple times.
  useFileHash: false,

  // Don't change this, unless you know what you're doing
  moduleSystem: 'commonjs',
};

module.exports = config;
