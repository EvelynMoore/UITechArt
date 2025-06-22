extends Button

@export var distanceToMove: float = 20.0;
@export var duration: float = 0.1;
@export var distToTeleport: float = 300;
@export var waitBeforeInitMove: float = 0.0;
@export var initMoveDuration= 0.5;
var original_position: Vector2
var tween

func _ready():
	original_position = position
	position.x -= distToTeleport;
	tween = get_tree().create_tween()
	tween.tween_interval(waitBeforeInitMove)
	tween.tween_property(self, "position", original_position, initMoveDuration)\
		.set_trans(Tween.TRANS_SINE)\
		.set_ease(Tween.EASE_IN_OUT)

func _on_mouse_entered():
	if tween: tween.kill()
	tween = get_tree().create_tween()
	tween.tween_property(self, "position", original_position + Vector2(distanceToMove, 0), duration)\
		.set_trans(Tween.TRANS_SINE)\
		.set_ease(Tween.EASE_IN_OUT)

func _on_mouse_exited():
	if tween: tween.kill()
	tween = get_tree().create_tween()
	tween.tween_property(self, "position", original_position, duration)\
		.set_trans(Tween.TRANS_SINE)\
		.set_ease(Tween.EASE_IN_OUT)
