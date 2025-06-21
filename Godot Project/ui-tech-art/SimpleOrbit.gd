extends Camera3D

@export var target: Node3D  # Assign the object to orbit around in the Inspector
@export var distance: float = 5.0
@export var orbit_speed: float = 1.0
@export var height: float = 2.0

var angle := 0.0

func _process(delta):
	if target:
		angle += orbit_speed * delta
		var x = cos(angle) * distance
		var z = sin(angle) * distance
		global_position = target.global_position + Vector3(x, height, z)
		look_at(target.global_position)
