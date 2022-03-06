db.createUser(
    {
        user: "usrCensusApp",
        pwd: "z1x2c3v4b5n6m7",
        roles: [
            {
                role: "readWrite",
                db: "dbCensusApp"
            }
        ]
    }
);