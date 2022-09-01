import api from "./api.ts"
const qs = require('qs')

export default {
  ReportNFDepartureDayByUF: (dateStart, dateEnd) => {
    return api.post('/ReportNFDepartureDayByUF/' + dateStart + '&' + dateEnd)
  },
  Files: () => {
    return api.get('/Files')
  }

}
