export default{
    loginRules() {
        return [
            v => v.length >= 4 || 'Min. 4 Caracteres',
            v => !!v || 'Login é Obrigatório',
            v => (v && v.length <= 10) || 'Min. 10 Caracteres'
        ]
    },
    emailRules() {
            return [
                v => !!v || 'E-mail é Obrigatório.',
                v => /.+@.+\..+/.test(v) || 'E-mail Invalido',
            ]
    },
    parseDate (date) {
        if (!date) return null

        const [day, month, year] = date.split('/')
        return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`
    },  

    formatDate (date) {
        if (!date) return null

        const [year, month, day] = date.split('-')
        return `${day}/${month}/${year}`
    }    
}