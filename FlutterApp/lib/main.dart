import 'package:flutter/material.dart';
import 'package:flutter_map/flutter_map.dart';
import 'package:latlong2/latlong.dart';
import 'package:positioned_tap_detector_2/positioned_tap_detector_2.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        // This is the theme of your application.
        //
        // Try running your application with "flutter run". You'll see the
        // application has a blue toolbar. Then, without quitting the app, try
        // changing the primarySwatch below to Colors.green and then invoke
        // "hot reload" (press "r" in the console where you ran "flutter run",
        // or simply save your changes to "hot reload" in a Flutter IDE).
        // Notice that the counter didn't reset back to zero; the application
        // is not restarted.
        primarySwatch: Colors.blue,
      ),
      home: const MyHomePage(title: 'Flutter Demo Home Page'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({Key? key, required this.title}) : super(key: key);

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  List<Marker> markers = <Marker>[
    Marker(
      point: LatLng(34.6857615848013, 135.52573444889032), //場所
      builder: (ctx) => GestureDetector(
        onTap: () {
          ScaffoldMessenger.of(ctx).showSnackBar(const SnackBar(
            content: Text("大阪城"),
          ));
        },
        child: const Icon(
          Icons.castle,
          color: Colors.redAccent,
        ),
      ),
    ),
    Marker(
      point: LatLng(34.702749991520044, 135.4958862250534), //場所
      builder: (ctx) => GestureDetector(
        onTap: () {
          ScaffoldMessenger.of(ctx).showSnackBar(const SnackBar(
            content: Text("大阪駅"),
          ));
        },
        child: const Icon(
          Icons.train,
        ),
      ),
    ),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('テスト')),
      body: Padding(
        padding: const EdgeInsets.all(8),
        child: Column(
          children: [
            const Padding(
              padding: EdgeInsets.only(top: 8, bottom: 8),
              child: Text('Try tapping on the markers'),
            ),
            Flexible(
              child: FlutterMap(
                  options: MapOptions(
                      center: LatLng(34.6857615848013, 135.52573444889032),
                      zoom: 13,
                      onTap: _handleTap),
                  layers: [
                    TileLayerOptions(
                      urlTemplate:
                          'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
                      subdomains: ['a', 'b', 'c'],
                      userAgentPackageName: 'dev.fleaflet.flutter_map.example',
                    ),
                    MarkerLayerOptions(markers: markers),
                  ]),
            ),
          ],
        ),
      ),
    );
  }

  void _handleTap(TapPosition tapPosition, LatLng latlng) {
    setState(() {
      markers.add(Marker(
        point: latlng,
        builder: (ctx) => GestureDetector(
          onTap: () {
            ScaffoldMessenger.of(ctx).showSnackBar(SnackBar(
                content: Text('地点 (' +
                    latlng.latitude.toStringAsPrecision(4) +
                    "." +
                    latlng.longitude.toStringAsPrecision(4) +
                    ')')));
          },
          child: const Icon(
            Icons.place,
          ),
        ),
      ));
    });
  }
}
