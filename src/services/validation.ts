export function loginRules() {
        return [
            v => v.length >= 4 || 'Min. 4 Caracteres',
            v => !!v || 'Login é Obrigatório',
            v => (v && v.length <= 10) || 'Min. 10 Caracteres'
        ]
    }
export function emailRules() {
        return [
            v => !!v || 'E-mail é Obrigatório.',
            v => /.+@.+\..+/.test(v) || 'E-mail Invalido',
        ]
    }