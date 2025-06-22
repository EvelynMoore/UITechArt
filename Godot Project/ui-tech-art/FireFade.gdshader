shader_type canvas_item;

uniform sampler2D noise_texture;
uniform float dissolve_amount : hint_range(-1.0, 1.0) = 0.0;
uniform float noise_scale = 8.0;
uniform vec4 dissolve_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);

void fragment() {
    vec2 uv = UV;

    vec4 tex_color = texture(TEXTURE, uv);

    float noise = texture(noise_texture, uv * noise_scale).r;

    float directional_bias = -uv.x;

    float dissolve_value = noise + directional_bias - dissolve_amount;

    if (dissolve_value < 0.0) {
        discard;
    }

    if (dissolve_value < 0.1) {
        COLOR = dissolve_color * 10.0;
		COLOR.a = dissolve_color.a * tex_color.a;
    } else {
        COLOR = tex_color;
    }
}
