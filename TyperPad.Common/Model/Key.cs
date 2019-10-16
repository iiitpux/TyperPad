namespace TyperPad.Common.Model
{
    public partial class Key
    {
        public Key(string keyword)
        {
            Keyword = keyword;
        }

        public bool IsModificator { set; get; } = false;
        public string Keyword { private set; get; }

        public override string ToString()
        {
            return Keyword;
        }

        //todo- атрибут отображения, то есть какая это буква или символ, в зависимосьт от языка, только в отдельном файле
        public static Key Key1 => new Key(nameof(Key1));
        public static Key Key2 => new Key(nameof(Key2));
        public static Key Key3 => new Key(nameof(Key3));
        public static Key Key4 => new Key(nameof(Key4));
        public static Key Key5 => new Key(nameof(Key5));
        public static Key Key6 => new Key(nameof(Key6));
        public static Key Key7 => new Key(nameof(Key7));
        public static Key Key8 => new Key(nameof(Key8));
        public static Key Key9 => new Key(nameof(Key9));
        public static Key Key0 => new Key(nameof(Key0));
        
        public static Key KeyA => new Key(nameof(KeyA));
        public static Key KeyB => new Key(nameof(KeyB));
        public static Key KeyC => new Key(nameof(KeyC));
        public static Key KeyD => new Key(nameof(KeyD));
        public static Key KeyE => new Key(nameof(KeyE));
        public static Key KeyF => new Key(nameof(KeyF));
        public static Key KeyG => new Key(nameof(KeyG));
        public static Key KeyH => new Key(nameof(KeyH));
        public static Key KeyI => new Key(nameof(KeyI));
        public static Key KeyJ => new Key(nameof(KeyJ));
        public static Key KeyK => new Key(nameof(KeyK));
        public static Key KeyL => new Key(nameof(KeyL));
        public static Key KeyM => new Key(nameof(KeyM));
        public static Key KeyN => new Key(nameof(KeyN));
        public static Key KeyO => new Key(nameof(KeyO));
        public static Key KeyP => new Key(nameof(KeyP));
        public static Key KeyQ => new Key(nameof(KeyQ));
        public static Key KeyR => new Key(nameof(KeyR));
        public static Key KeyS => new Key(nameof(KeyS));
        public static Key KeyT => new Key(nameof(KeyT));
        public static Key KeyU => new Key(nameof(KeyU));
        public static Key KeyV => new Key(nameof(KeyV));
        public static Key KeyW => new Key(nameof(KeyW));
        public static Key KeyX => new Key(nameof(KeyX));
        public static Key KeyY => new Key(nameof(KeyY));
        public static Key KeyZ => new Key(nameof(KeyZ));
        
        public static Key Minus => new Key(nameof(Minus));
        public static Key Equal => new Key(nameof(Equal));
        public static Key Backspace => new Key(nameof(Backspace));
        public static Key Tab => new Key(nameof(Tab));
        public static Key BraceLeft => new Key(nameof(BraceLeft));
        public static Key BraceRight => new Key(nameof(BraceRight));
        public static Key BackSlash => new Key(nameof(BackSlash));
        public static Key Semicolon => new Key(nameof(Semicolon));
        public static Key Quote => new Key(nameof(Quote));
        public static Key Enter => new Key(nameof(Enter));
        public static Key Comma => new Key(nameof(Comma));
        public static Key Dot => new Key(nameof(Dot));
        public static Key Slash => new Key(nameof(Slash));
        public static Key Space => new Key(nameof(Space));
        public static Key Home => new Key(nameof(Home));
        public static Key End => new Key(nameof(End));
        public static Key Delete => new Key(nameof(Delete));
        public static Key Pause => new Key(nameof(Pause));
        public static Key Break => new Key(nameof(Break));
        public static Key PrintScreen => new Key(nameof(PrintScreen));
        public static Key PageUp => new Key(nameof(PageUp));
        public static Key PageDown => new Key(nameof(PageDown));
        public static Key ArrowUp => new Key(nameof(ArrowUp));
        public static Key ArrowDown => new Key(nameof(ArrowDown));
        public static Key ArrowLeft => new Key(nameof(ArrowLeft));
        public static Key ArrowRight => new Key(nameof(ArrowRight));
        public static Key Capital => new Key(nameof(Capital));
        public static Key Insert => new Key(nameof(Insert));
        public static Key Esc => new Key(nameof(Esc));
        public static Key F1 => new Key(nameof(F1));
        public static Key F2 => new Key(nameof(F2));
        public static Key F3 => new Key(nameof(F3));
        public static Key F4 => new Key(nameof(F4));
        public static Key F5 => new Key(nameof(F5));
        public static Key F6 => new Key(nameof(F6));
        public static Key F7 => new Key(nameof(F7));
        public static Key F8 => new Key(nameof(F8));
        public static Key F9 => new Key(nameof(F9));
        public static Key F10 => new Key(nameof(F10));
        public static Key F11 => new Key(nameof(F11));
        public static Key F12 => new Key(nameof(F12));
        public static Key VolumeMute => new Key(nameof(VolumeMute));
        public static Key VolumeDown => new Key(nameof(VolumeDown));
        public static Key VolumeUp => new Key(nameof(VolumeUp));
        public static Key MediaNextTrack => new Key(nameof(MediaNextTrack));
        public static Key MediaPrevTrack => new Key(nameof(MediaPrevTrack));
        public static Key MediaStop => new Key(nameof(MediaStop));
        public static Key MediaPause => new Key(nameof(MediaPause));

        public static Key Control => new Key(nameof(Control)) {IsModificator = true};
        public static Key Alt => new Key(nameof(Alt)) {IsModificator = true};
        public static Key Shift => new Key(nameof(Shift)) {IsModificator = true};
        public static Key Win => new Key(nameof(Win)) {IsModificator = true};
    }
}