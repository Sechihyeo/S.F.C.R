const express = require('express')
const knex = require('knex')
const app = express()

const db = knex({ client: 'mysql', connection: { host: 'localhost', user: 'sfc', database: 'sfc' } })

app.get('/kdr/info', async (req, res) => {
  const { id } = req.query
  if (!id) return res.send('ID Undefind').status(404)
  if (!Number(id)) return res.send('Wrong Type').status(404)
  const [exist] = await db.where({ id }).from('user').select('*')
  if (!exist) return res.send('Can not get user').status(404)
  const kdr = exist.k / exist.d
  res.send(kdr.toString()).status(200)
})

app.get('/kdr/push', async (req, res) => {
  const { id, kill, death } = req.query
  if (!id || !kill || !death) return res.send('Undefind Query').status(404)
  if (!Number(id) || !Number(kill) || !Number(death)) return res.send('Wrong Type').status(404)
  const [exist] = await db.where({ id }).from('user').select('*')
  if (!exist) await db.insert({ id, k: kill, d: death }).from('user').select('*')
  else await db.update({ k: exist.k + kill, d: exist.d + death }).from('user').select('*').where({ id })
  res.sendStatus(200)
})

app.listen(8080, () => console.log('Server is now online'))
