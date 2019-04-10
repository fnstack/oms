open Saturn
open OMS.API

let app =
    initDb()
    application {
        url "http://0.0.0.0:8086"
        use_router appRouter
    }

run app
