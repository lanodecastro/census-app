db.createUser(
    {
        user: "usrCensusApp",
        pwd: "1025@1064",
        roles: [
            {
                role: "readWrite",
                db: "dbCensusApp"
            }
        ]
    }
);