<template>
  <div>
    <l-map ref="map" style="height: 400px" :zoom="zoom" :center="center">
      <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>
      <l-polyline
        v-for="polyline in polylines"
        :key="polyline.id"
        :lat-lngs="polyline.latlngs"
        :color="getColor(polyline.level)"
      ></l-polyline>
    </l-map>
  </div>
</template>

<script>
import { LMap, LTileLayer, LPolyline } from 'vue2-leaflet'
import { segmentsService } from '@/services/segmentsService.js'
import { ui } from '../externalization/uiTextPlugin.js'

export default {
  components: {
    LMap,
    LTileLayer,
    LPolyline
  },
  data () {
    return {
      url: 'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
      attribution: 'TP3 app web',
      zoom: 12,
      center: [46.81, -71.22], // le centre est qc pour l'instant
      polylines: []
    }
  },
  computed: {
    trail () {
      return this.$store.state.map.selectedTrail
    }
  },
  methods: {
    setNewFocus () {
      var group = []
      for (let i = 0; i < this.polylines.length; i++) {
        for (let j = 0; j < this.polylines[i].latlngs.length; j++) {
          group.push(this.polylines[i].latlngs[j])
        }
      }
      // permet le changement du zoom et de la position
      this.$refs.map.mapObject.fitBounds(group)
    },
    async loadPolyline () {
      var newLat = []
      var autoIncrementId = 0
      let hasError = false
      await this.trail.segments.forEach(async function (id) {
        try {
          const segments = await segmentsService.getSegmentById(id)
          autoIncrementId++
          const polyline = {
            id: autoIncrementId,
            latlngs: segments.coordinates,
            level: segments.level
          }
          newLat.push(polyline)
        } catch (error) {
          hasError = true
        }
      })
      if (hasError) {
        this.makeToast(ui.Map.CANT_LOAD_TRAIL, ui.SERVER_ERROR_TITLE)
      } else {
        this.polylines = newLat
      }
    },
    getColor (color) {
      if (color === 'Facile') {
        return 'green'
      } else if (color === 'Intermédiaire') {
        return 'blue'
      } else if (color === 'Difficile') {
        return 'orange'
      } else if (color === 'Très difficile') {
        return 'red'
      } else {
        return 'black'
      }
    },
    makeToast (msg, title) {
      this.$bvToast.toast(msg, {
        title: title,
        autoHideDelay: 5000,
        appendToast: true
      })
    }
  },
  watch: {
    trail: function () {
      if (this.trail !== null) {
        this.loadPolyline()
      }
    },
    polylines: function () {
      // if nécessaire pour la population longue du array
      // qui n'était pas fonctionnel avec un simple async/await
      if (this.polylines.length === this.trail.segments.length) {
        this.setNewFocus()
      }
    }
  }
}
</script>
