import api from "./api.ts"
const qs = require('qs')

export default {
  ReportCFDay: (dateStart, dateEnd) => {
    return api.post('/ReportCFDay?dateStart=' + dateStart + '&dateEnd=' + dateEnd)
  },
  ReportNFDay: (dateStart, dateEnd) => {
    return api.post('/ReportNFDay?dateStart=' + dateStart + '&dateEnd=' + dateEnd)
  },
  ReportNFDayByICMS: (dateStart, dateEnd) => {
    return api.post('/ReportNFDayByICMS?dateStart=' + dateStart + '&dateEnd=' + dateEnd)
  },
  ReportNFDayByICMSST: (dateStart, dateEnd) => {
    return api.post('/ReportNFDayByICMSST?dateStart=' + dateStart + '&dateEnd=' + dateEnd)
  },
  ReportNFDepartureDayByRedZ: (dateStart, dateEnd) => {
    return api.post('/ReportNFDepartureDayByRedZ?dateStart=' + dateStart + '&dateEnd=' + dateEnd)
  },
  ReportNFDepartureDayByUF: (dateStart, dateEnd) => {
    return api.post('/ReportNFDayByUF?dateStart=' + dateStart + '&dateEnd=' + dateEnd)
  },
  ReportNFQtdDay: (dateStart, dateEnd) => {
    return api.post('/ReportNFQtdDay?dateStart=' + dateStart + '&dateEnd=' + dateEnd)
  },
  ReportNFQtdDayByUF: (dateStart, dateEnd) => {
    return api.post('/ReportNFQtdDayByUF?dateStart=' + dateStart + '&dateEnd=' + dateEnd)
  },
  ReportTotalizersDay: (dateStart, dateEnd) => {
    return api.post('/ReportTotalizersDay?dateStart=' + dateStart + '&dateEnd=' + dateEnd)
  },
  Files: () => {
    return api.get('/Files')
  }

}
