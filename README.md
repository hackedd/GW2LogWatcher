# GW2 Log Watcher

GW2 Log Watcher will keep an eye on your [arcdps](http://www.deltaconnected.com/arcdps/) log folder. When it sees any new logs, it will automatically run [RaidHeroes](https://raidheroes.tk/), upload your logs to [dps.report](https://dps.report/) and upload them to [GW2 Raidar](https://www.gw2raidar.com/). This way, you don't have to do it yourself. Automate all the things!

![Screenshot](screenshot.png?raw=true)

## Tests

The tests will need some preparation to work correctly:

* Download RaidHeroes and extract it to `Test Files\RaidHeroes`
* Copy `Test Files\test.config.dist` to `Test Files\test.config`
* Create a GW2Raidar account and put the credentials in `Test Files\test.config`

You should now be able to run the tests. Some of them, especially the ones for GW2Raidar, will take a while to complete.
