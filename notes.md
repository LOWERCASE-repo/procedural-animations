# todo
- switch totalsize to totalmass
- inc distance for walkers

# refactor
- core and probe extend mover
- all proc anims are core and probe based
- core scans for targets, distributes to probes
	- classes that extend core change the scanbox
- whatever connects the core and probes is just a cosmetic hitbox
	- connects to the probe's dynamic body and a kinematic body on the core
- probe spawns and manages connection, extensions have diff connections
