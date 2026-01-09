# Examples

This folder is the playground for small, self-contained console apps that demonstrate
ideas and generate sample output. Each app lives in its own folder.

## Apps

Current apps: `piecewise_constant_demo`, `example_hello_world`, `mersenne_twister_32`.

### piecewise_constant_demo/

C++ demo for `std::piecewise_constant_distribution<double>` using interval weights, sample output,
and interval proportions.

### example_hello_world/

Minimal C++ hello-world used to verify the build pipeline.

### mersenne_twister_32/

C console app that produces MT19937 (32-bit) output for test data or comparison runs.

## Build and Run

From the repo root:

```sh
make -C examples
./examples/build/piecewise_constant_demo
./examples/build/example_hello_world
./examples/build/mersenne_twister_32
```

You can also build a single app by name:

```sh
make -C examples piecewise_constant_demo
./examples/build/piecewise_constant_demo
```

The Mersenne Twister console app accepts optional arguments:

```sh
make -C examples mersenne_twister_32
./examples/build/mersenne_twister_32 5489 10
```

To clean:

```sh
make clean
```
